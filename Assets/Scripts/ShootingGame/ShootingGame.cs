using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGame : MonoBehaviour
{
    [SerializeField] List<Transform> targets;
    List<IShootingTarget> shootingTargets;
    bool isReady;
    bool isPlaying;
    [SerializeField] Animator courtainAnimator;
    [SerializeField] Rigidbody gun;
    [SerializeField] Transform gunStartPoint;
    [SerializeField] ShootingGameScoreDisplay currentScoreUI;
    ShootingGameScore currentScore;
    [SerializeField] ShootingGameScoreDisplay highScoreUI;
    ShootingGameScore highScore;


    void Start()
    {
        ResetGun();
        GetTargets();
        TurnOffTargets();
        InitializeScore();
    }

    private void GetTargets()
    {
        shootingTargets = new List<IShootingTarget>();
        foreach (Transform t in targets)
        {
            if (t.gameObject.TryGetComponent<IShootingTarget>(out IShootingTarget target))
            {
                shootingTargets.Add(target);
            }
        }
    }

    public void StartGame()
    {
        if(isPlaying){return;}
        StartCoroutine(GameRoutine());
    }

    int remainingTargets;
    IEnumerator GameRoutine()
    {
        isPlaying = true;
        isReady = false;
        int round = 1;
        
        currentScore = new();
        currentScoreUI.DisplayScore(currentScore);
        
        while(round <= 3)
        {
            //Turn of all targets
            TurnOffTargets();
            //5t    10t  20t
            //10s   15s  20s
            //Copy to manipulate non-selected targets
            List<IShootingTarget> inactiveTargets = new List<IShootingTarget>(shootingTargets);
            int totalTargets = round == 3? 20 : round * 5;
            remainingTargets = totalTargets;
            //Activate small targets
            for (int i = 0; i < totalTargets; i++)
            {
                int index = Random.Range(0, inactiveTargets.Count);
                inactiveTargets[index].Activate();
                inactiveTargets.RemoveAt(index);
            }
            
            float waitTime = 5 + (round * 5);

            //Start animation
            courtainAnimator.Play("Open");
            //Start game when courtains fully open
            yield return new WaitUntil(() => isReady);
            isReady = false;
            float nextTime = Time.time + waitTime;
            yield return new WaitUntil(()=> nextTime < Time.time || remainingTargets <= 0);
            courtainAnimator.Play("Close");
            yield return new WaitUntil(() => isReady);
            isReady = false;
            round++;
        }
        isPlaying = false;
        ResetGun();
        SaveScore();    
    }

    public void SetReady()
    {
        isReady = true;
    }

    private void TurnOffTargets()
    {
        foreach (IShootingTarget t in shootingTargets)
        {
            t.Deactivate();
        }
    }
    
    public void ResetGun()
    {
        gun.gameObject.SetActive(false);
        gun.transform.SetPositionAndRotation(gunStartPoint.position,gunStartPoint.rotation);
        gun.linearVelocity = Vector3.zero;
        gun.angularVelocity = Vector3.zero;
    }

    private const string TARGETS =  "shootingTargets";
    private const string BULLETS =  "highBullets";
    void InitializeScore()
    {
        currentScore = new();        
        currentScoreUI.DisplayScore(currentScore);

        highScore = new()
        {
            targetsHit = PlayerPrefs.GetInt(TARGETS, 0),
            bulletsUsed = PlayerPrefs.GetInt(BULLETS, 0)
        };

        highScoreUI.DisplayScore(highScore);
    }

    void SaveScore()
    {
        //Less targets hit
        if(currentScore.targetsHit < highScore.targetsHit){return;}
        
        //Same targets hit, but used more bullets
        if(currentScore.targetsHit == highScore.targetsHit && highScore.bulletsUsed < currentScore.bulletsUsed){return;}

        //Register high score
        highScore.targetsHit = currentScore.targetsHit;
        highScore.bulletsUsed = currentScore.bulletsUsed;

        PlayerPrefs.SetInt(TARGETS,highScore.targetsHit);
        PlayerPrefs.SetInt(BULLETS,highScore.bulletsUsed);

        highScoreUI.DisplayScore(highScore);
    }

    public void ShootBullet()
    {
        currentScore.bulletsUsed++;
        currentScoreUI.DisplayScore(currentScore);
    }

    public void HitTarget()
    {
        currentScore.targetsHit++;
        currentScoreUI.DisplayScore(currentScore);
        remainingTargets--;
    }
}


