using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour {

    private GameObject m_cModel;
    private GameObject m_cHologram;

    public void SetModels(GameObject model, GameObject hologram)
    {
        m_cModel = model;
        m_cHologram = hologram;
    }

    public void SwitchModelToHologram()
    {
        m_cModel.SetActive(false);
        m_cHologram.SetActive(true);
    }

    public void SwitchHologramToModel()
    {
        m_cModel.SetActive(true);
        m_cHologram.SetActive(false);
    }

    public void EnableModel(bool enable)
    {
        m_cModel.SetActive(enable);
    }

    public void EnableHologram(bool enable)
    {
        m_cHologram.SetActive(enable);
    }
}
