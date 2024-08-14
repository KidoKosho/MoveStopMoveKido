using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ArrowUI arrowPrefab;
    [SerializeField] private Canvas canvas;
    private Level currentLevel;
    private List<Character> characterList = new List<Character>();
    public Level[] levelPrefabs;
    public Player player;
    public int CharacterAmount => currentLevel.botCount + 1;
    private int level = 0;
    public List<Character> CharacterList => characterList;
    public float length => currentLevel.length;
    public float width => currentLevel.width;
    public int countInstanceCharacter;
    private int countcharacterdie;
    public int Alive => CharacterAmount - countcharacterdie;

    public void OnInit()
    {
        countcharacterdie = countInstanceCharacter = 0;
        for(int i =0;i < (currentLevel.CharacterPlayingGame+1>CharacterAmount?CharacterAmount:currentLevel.CharacterPlayingGame+1);i++)
        { 
            if(i == 0)
            {
                player.transform.position = currentLevel.position;
                player.gameObject.SetActive(true);
                if (player.Arrow == null)
                {
                    ArrowUI arrow = Instantiate(arrowPrefab, canvas.transform);
                    arrow.gameObject.SetActive(false);
                    player.Changearrow(arrow);
                }
                player.OnInit();
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
                Add(player);
            }
            else
            {
                InstanceCharacter();
            }
            countInstanceCharacter++;
        }
    }
    public void NextLevel()
    {
        level++;
        OnLoadLevel();
    }
    public void OnLoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level], transform);
            currentLevel.gameObject.SetActive(true);
            OnInit();
        }
        else
        {

        }
    }
    public void OnStartGame()
    {
        OnLoadLevel() ;
    }
    public void OnFinishGame()
    {
        OnDespawn();
    }
    public void OnDespawn()
    {
        CancelInvoke();
        Clear();
        BotManager.Ins.Clear();
    }
    public void OnReset()
    {
        OnDespawn();
        OnLoadLevel();
    }
    public Vector3 FindCharacter(Character character)
    {

        Vector3 sol = character.CharacterTransform.position;
        if(!player.IsDead) sol = player.CharacterTransform.position;
        return sol;
    }
    public void RemoveListCharacter(Character character)
    {
        if(character is Enemy) BotManager.Ins.Remove(character as Enemy);
        //for(int i = 0;i<characterList.Count ; ++ i)
        //{
        //    if (characterList[i] == character)
        //    {
        //        characterList.RemoveAt(i);
        //        break;
        //    }
        //}
        characterList.Remove(character);
        ++ countcharacterdie;
        if (countInstanceCharacter < CharacterAmount)
        {
            ++countInstanceCharacter;
            Invoke(nameof(InstanceCharacter),0.3f);
        }
    }
    public void CharacterDead(Character character)//TODO : tach ra lai ham rieng ,tranh dung if nhieu lan
    {
        //if(!GameManger.Ins.Isplay) return;
        if (character is Player)
        {
            GameManger.Ins.ChangePlay(false);
            UIManager.Ins.OpenUI<CanvasLose>().ShowCanvas(countcharacterdie + 1, (float)(countcharacterdie + 2) / (float)(CharacterAmount));
        }
        else if (characterList.Count == 1 && countInstanceCharacter >= CharacterAmount)
        {
            GameManger.Ins.ChangePlay(false);
            if (characterList[0] is Player)
            {
                UIManager.Ins.OpenUI<CanvasWin>().ShowCanvas(countcharacterdie + 1, (float)(countcharacterdie + 2) / (float)(CharacterAmount));
            }
        }
        
    }
    public void Clear()
    {

        SimplePool.CollectAll();
        for (int i = characterList.Count - 1; i >=0 ; --i)
        {
            characterList[i].OnDespawn();
            if (characterList[i] is Player)characterList[i].gameObject.SetActive(false);
            characterList.RemoveAt(i);
        }
    }
    public void Add(Character character)
    {
        CharacterList.Add(character);
    }
    public void InstanceCharacter()
    {
        Vector3 enemytransform;
        do
        {
            float x = UnityEngine.Random.Range(-currentLevel.length / 2, currentLevel.length / 2);
            float z = UnityEngine.Random.Range(-currentLevel.width / 2, currentLevel.width / 2);
            enemytransform = new Vector3(x, currentLevel.position.y, z);
        } while (Vector3.Distance(enemytransform, player.transform.position) <= 7f);
        BotManager.Ins.Add(enemytransform, countInstanceCharacter > CharacterAmount - 4 ? 1 : 0,player.LevelCharacter, arrowPrefab, canvas);
    }
    private void Update()//TODO : dua ve class cu the 
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            Vector3 viewPosition = mainCamera.WorldToViewportPoint(characterList[i].CharacterTransform.position);
            if (!GameManger.Ins.Isplay || (viewPosition.x > 0f && viewPosition.x < 1f && viewPosition.y > 0f && viewPosition.y < 1f)) {
                characterList[i].Arrow.gameObject.SetActive(false);
            }
            else
            {
                if (viewPosition.z < 0f)
                {
                    // Nếu z âm, đối tượng nằm sau camera. Xử lý trường hợp này nếu cần thiết
                    viewPosition = Vector3Invert(viewPosition);
                    viewPosition = Vector3FixEdge(viewPosition);
                }
                viewPosition.x = Mathf.Clamp(viewPosition.x, 0.01f, 0.99f);
                viewPosition.y = Mathf.Clamp(viewPosition.y, 0.01f, 0.99f);
                Vector3 screenPosition = mainCamera.ViewportToScreenPoint(viewPosition);
                characterList[i].Arrow.gameObject.SetActive(true);
            // Xoay mũi tên hướng về đối tượng
                Vector3 screenPosCharacter = mainCamera.WorldToScreenPoint(characterList[i].CharacterTransform.position);
                Vector3 screenPosPlayer = mainCamera.WorldToScreenPoint(player.CharacterTransform.position);
                Vector3 direction = screenPosCharacter - screenPosPlayer;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // Đặt anchor của mũi tên tại vị trí thích hợp
                characterList[i].Arrow.LoadUpdate(screenPosition, Quaternion.Euler(new Vector3(0, 0, angle - 90)), viewPosition, characterList[i].LevelCharacter);
            }
        }
    }
    public Vector3 Vector3FixEdge(Vector3 vector)
    {
        Vector3 vectorFixed = vector;
        float highestValue = Vector3Max(vectorFixed);
        float lowerValue = Vector3Min(vectorFixed);

        float highestValueBetween = DirectionPreference(lowerValue, highestValue);
        if (highestValueBetween == highestValue)
        {

            vectorFixed.x = vectorFixed.x == highestValue ? 1 : vectorFixed.x;
            vectorFixed.y = vectorFixed.y == highestValue ? 1 : vectorFixed.y;
        }
        if (highestValueBetween == lowerValue)
        {

            vectorFixed.x = Mathf.Abs(vectorFixed.x) == lowerValue ? 0 : Mathf.Abs(vectorFixed.x);
            vectorFixed.y = Mathf.Abs(vectorFixed.y) == lowerValue ? 0 : Mathf.Abs(vectorFixed.y);
        }
        return vectorFixed;
    }
    Vector3 Vector3Invert(Vector3 viewport_position)
    {
        Vector3 invertedVector = viewport_position;
        invertedVector.x = 1f - invertedVector.x;
        invertedVector.y = 1f - invertedVector.y;
        invertedVector.z = 0;
        return invertedVector;
    }
    float Vector3Max(Vector3 vector)
    {

        float highestValue = Mathf.Max(vector.x, vector.y);
        return highestValue;
    }
    float Vector3Min(Vector3 vector)
    {
        float lowerValue = 0f;
        lowerValue = vector.x <= lowerValue ? vector.x : lowerValue;
        lowerValue = vector.y <= lowerValue ? vector.y : lowerValue;

        return lowerValue;
    }
    float DirectionPreference(float lowerValue, float highestValue)
    {
        lowerValue = Mathf.Abs(lowerValue);
        highestValue = Mathf.Abs(highestValue);


        //Obetendo maior valor entre os dois
        float highestValueBetween = Mathf.Max(lowerValue, highestValue);

        return highestValueBetween;
    }
}
