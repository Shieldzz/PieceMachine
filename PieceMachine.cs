using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMachine : MonoBehaviour
{
    private PieceManager m_cPieceManager;

    [SerializeField]
    private GameObject m_cPieceSpawn = null;

    private GameObject[] m_cPieces;
    private int m_iPiecesCount = 0;

    private Hologram[] m_cHologramComponent;

    [SerializeField]
    private float m_fTimeBetweenTwoPiece = 1.5f;
    private float m_fTimer = 0.0f;

    private int m_iIndexPiece = 0;

	void Start ()
    {
        m_cPieceManager = PieceManager.Instance;
        m_iPiecesCount = m_cPieceManager.PieceCount;

        m_cPieces = new GameObject[m_iPiecesCount];
        m_cPieces = m_cPieceManager.GetPieces();

        m_cHologramComponent = new Hologram[m_iPiecesCount];
        for (int i = 0; i < m_iPiecesCount; i++)
        {
            GameObject piece = m_cPieces[i];
            piece.transform.parent = m_cPieceSpawn.transform;
            piece.transform.localPosition = Vector3.zero;
            piece.transform.localRotation = Quaternion.identity;

            Hologram hologram = piece.GetComponent<Hologram>();
            hologram.SwitchModelToHologram();
            m_cHologramComponent[i] = hologram;
        }

        m_fTimer = m_fTimeBetweenTwoPiece;
	}
	
	void Update ()
    {
        m_fTimer -= Time.deltaTime;

		if (m_fTimer < 0)
        {
            SwitchPiece();
            m_fTimer = m_fTimeBetweenTwoPiece;
        }
	}

    private void SwitchPiece()
    {
        m_cHologramComponent[m_iIndexPiece].EnableHologram(false);

        m_iIndexPiece = (m_iIndexPiece + 1) % m_iPiecesCount;

        m_cHologramComponent[m_iIndexPiece].EnableHologram(true);
    }

    public GameObject GetPieceMachine()
    {
        return Instantiate(m_cPieces[m_iIndexPiece]);
    }
}
