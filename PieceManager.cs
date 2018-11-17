using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {

    private static PieceManager m_cInstance = null;
    public static PieceManager Instance
    {
        get { return m_cInstance; }
    }

    [SerializeField]
    private GameObject m_cPiecePrefab = null;

    [SerializeField]
    private PieceSettings[] m_lcPieceSettings;
    private int m_iPieceSettingsCount;
    public int PieceCount
    {
        get { return m_iPieceSettingsCount; }
    }

    private List<Sprite> m_lcSprite = new List<Sprite>();

    GameManager m_cInstanceGameMgr;

    private void Awake()
    {
        if (m_cInstance == null)
            m_cInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        m_cInstanceGameMgr = GameManager.Instance;
        m_iPieceSettingsCount = m_lcPieceSettings.Length;

        PreloadAllPieces();
    }

    private void Start()
    {
        for (int i = 0; i < m_cInstanceGameMgr.Players.Length; i++)
        {
            PlayerUI playerUI = m_cInstanceGameMgr.Players[i].PlayerUI;
            if (playerUI)
                playerUI.InstanciateSprite(InstantiateAllSpritePieces(playerUI.transform));
        }
    }

    void PreloadAllPieces()
    {
        //for (int i = 0; i < m_iPieceSettingsCount; i++)
        //{
        //    PieceSettings piece = m_lcPieceSettings[i];
        //    if (piece.m_cPrefabPiece)
        //        PoolManager.Preload(piece.m_cPrefabPiece, transform, piece.m_iNomberOfPiece);
        //}
    }

    List<GameObject> InstantiateAllSpritePieces(Transform parent)
    {
        List<GameObject> spriteList = new List<GameObject>();
        for (int i = 0; i < m_iPieceSettingsCount; i++)
        {
            PieceSettings piece = m_lcPieceSettings[i];
            if (piece.m_cSpriteUI)
            {
                GameObject spriteObject = Instantiate(piece.m_cSpriteUI, parent);
                spriteObject.transform.localPosition = Vector3.zero;
                spriteObject.transform.localRotation = Quaternion.identity;
                spriteList.Add(spriteObject);
            }
        }

        return spriteList;
    }

    public GameObject[] GetPieces()
    {
        GameObject[] pieces = new GameObject[m_iPieceSettingsCount];

        for (int i = 0; i < m_iPieceSettingsCount; i++)
        {
            pieces[i] = Instantiate(m_cPiecePrefab);
            GameObject model = Instantiate(m_lcPieceSettings[i].m_cPrefabModel, pieces[i].transform);
            GameObject hologram = Instantiate(m_lcPieceSettings[i].m_cPrefabHologram, pieces[i].transform);
            pieces[i].GetComponent<Hologram>().SetModels(model, hologram);
        }

        return pieces;
    }

    //public GameObject SpawnObject(int index, Transform parent)
    //{
    //    //GameObject pieceObject = PoolManager.Spawn(m_lcPieceSettings[index].m_cPrefabPiece, Vector3.zero, Quaternion.identity, parent);
    //    //piece.

    //    //return piece;

    //    //return PoolManager.Spawn(m_lcPieceSettings[index].m_cPrefabPiece, Vector3.zero, Quaternion.identity, parent);
    //}

    public void DespawnObject(GameObject gameObject)
    {
        PoolManager.Despawn(gameObject);
    }

}
