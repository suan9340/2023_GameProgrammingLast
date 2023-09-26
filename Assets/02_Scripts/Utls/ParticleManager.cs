using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region SingleTon

    private static ParticleManager _instance = null;
    public static ParticleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ParticleManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("ParticleManager").AddComponent<ParticleManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region 인터페이스

    //private GameObject momobj;
    public enum ParticleType
    {
        enemyDie,
        blood,
    }

    private void Start()
    {
        //momobj = GameObject.Find("Canvas (Effect)");
    }

    /// <summary>
    /// 파티클 복사본을 생성한다
    /// </summary>
    /// <param name="pt"> 파티클 이름 </param>
    /// <param name="pos"> 위치 </param>
    /// <returns></returns>
    /// "Resources" 폴더에서 바로 읽어 와서 Map에 담아 딱 하나의 원본만 들고 있도록 한다.
    public int AddParticle(ParticleType pt, Vector3 pos)
    {
        switch (pt)
        {
            case ParticleType.enemyDie:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/EnemyDieParticle");
                }
                break;


            case ParticleType.blood:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/Blood");
                }
                break;

            default:
                Debug.LogWarning("아직 연결하지 않은 파티클");
                return 0;
        }

        if (particleDic[pt] == null)
        {
            Debug.LogWarning($"로딩을 못했!!! {pt}");
            return 0;
        }

        // 해당 파티클의 복사본 생성!
        GameObject go = Instantiate<GameObject>(particleDic[pt], pos, particleDic[pt].transform.rotation);
        //go.transform.SetParent(momobj.transform, false);

        return 0;
    }

    // 파티클 원본을 담아두자
    private static Dictionary<ParticleType, GameObject> particleDic = new Dictionary<ParticleType, GameObject>();
    #endregion
}
