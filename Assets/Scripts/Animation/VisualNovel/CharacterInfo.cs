using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using NaughtyAttributes;

public class CharacterInfo : MonoBehaviour
{
    public List<Sprite> characterNameSprite = null;
    public bool isAlone = false;
    public bool pastDialogue;

    public bool gotLinkCharacter = false;
    [ShowIf("gotLinkCharacter")]
    public GameObject linkCharacter;
    
    public bool autoLaunch = false;
    [ShowIf("autoLaunch")]
    [SerializeField] private float waitForLaunchTime;
    public bool newChangement = false;

    [ShowIf("newChangement")]
    public List<GameObject> newObjects;
    [ShowIf("newChangement")]
    public List<GameObject> destroyWhenChangement;
    [ShowIf("newChangement")]
    public List<GameObject> objectsToInstantiate;

    [SerializeField] private List<Sprite> FirstCharacterSprites = null;
    
    public List<Sprite> firstCharacterSprites
    {
        get { return FirstCharacterSprites; }
        private set { FirstCharacterSprites = value; }
    }

    public bool anotherInterlocutor;
    [ShowIf("anotherInterlocutor")]
    [SerializeField] private List<Sprite> OtherToTalkSprites = null;
    
     public List<Sprite> otherToTalkSprites
    {
        get { return OtherToTalkSprites; }
        private set { OtherToTalkSprites = value; }
    }
    
    [SerializeField] private List<Sprite> PetraSprites;
    public List<Sprite> petraSprites
    {
        get { return PetraSprites; }
        private set { PetraSprites = value; }
    }
    [SerializeField] private List<DialogueLine> myDialogueList  = new List<DialogueLine>();
    
    public List<DialogueLine> dialogueList
    {
        get { return myDialogueList; }
        private set { myDialogueList = value; }
    }

    [SerializeField] private bool proofsPresentations = false;

    [ShowIf("proofsPresentations")]
    [SerializeField] private List<Present> myProofLists = new List<Present>();

    public List<Present> presentLists
    {
        
        get{return myProofLists;}
        private set {myProofLists = value;}
    }

    #region StartDialogue

    public IEnumerator waitForDialogueToStart()
    {
        yield return new WaitForSeconds(waitForLaunchTime);
        CameraManager.instance.virtualCameraZoom.m_Priority += 10;
        StartCoroutine(waitToEndZoom());
    }

    IEnumerator waitToEndZoom()
    {
        yield return new WaitForSeconds(CameraManager.instance.cinemachineBrain.m_DefaultBlend.m_Time);
        DialogueHandler.Instance.startDialogue(this, this.isAlone);
    }

    #endregion
}
