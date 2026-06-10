using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void Salvar(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
        Debug.Log("Jogo salvo!");
    }

    public static PlayerData Carregar()
    {
        if (PlayerPrefs.HasKey("PlayerData")){
            string json = PlayerPrefs.GetString("PlayerData");
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Jogo carregado!");
            return data;
        }
        Debug.Log("Nenhum save encontrado");
        return null;
    }

    public static void DeletarSave()
    {
        PlayerPrefs.DeleteKey("PlayerData");
        Debug.Log("Save deletado!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
