using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenuHandler : MonoBehaviour
{
    GameStateManager GameStateManager;
    Lobby Lobby;
    public GameObject background;

    public Sprite GreenWon;
    public Sprite RedWon;
    public Sprite YellowWon;
    public Sprite BlueWon;

    public Image PlayerWonImage;

    public MeshRenderer DishMesh;
    public MeshRenderer BarnMesh;
    public SkinnedMeshRenderer ChickenMesh;

    Color playerColor;

    void Start()
    {
        GameStateManager = FindObjectOfType<GameStateManager>();
        Lobby = FindObjectOfType<Lobby>();

        // Swap panel for correct one
        switch (GameStateManager.Winner.playerIndex)
        {
            case 0:
                PlayerWonImage.sprite = GreenWon;
                playerColor = Color.green;
                break;
            case 1:
                PlayerWonImage.sprite = RedWon;
                playerColor = Color.red;
                break;
            case 2:
                PlayerWonImage.sprite = YellowWon;
                playerColor = Color.yellow;
                break;
            case 3:
                PlayerWonImage.sprite = BlueWon;
                playerColor = Color.blue;
                break;
        }

        for (int i = 0; i < DishMesh.materials.Length; i++)
        {
            if (DishMesh.materials[i].name == "GreenFarmColorMaterial (Instance)")
            {
                Material mat = DishMesh.materials[i];
                mat.SetColor("_Color", playerColor);
                DishMesh.materials[i] = mat;
            }
        }
        for (int i = 0; i < BarnMesh.materials.Length; i++)
        {
            if (BarnMesh.materials[i].name == "GreenFarmColorMaterial (Instance)")
            {
                Material mat = BarnMesh.materials[i];
                mat.SetColor("_Color", playerColor);
                BarnMesh.materials[i] = mat;
            }
        }
        for (int i = 0; i < ChickenMesh.materials.Length; i++)
        {
            if (ChickenMesh.materials[i].name == "Mat_PlayerOwner (Instance)")
            {
                Material mat = ChickenMesh.materials[i];
                mat.SetColor("_Color", playerColor);
                ChickenMesh.materials[i] = mat;
            }
        }
    }

    void Update()
    {
        CheckForStartPressed();
    }

    void CheckForStartPressed()
    {
        for (int i = 0; i < 4; i++)
        {
            x360_Gamepad pad = GamepadManager.Instance.GetGamepad(i + 1);
            if (!pad.IsConnected) continue;
            bool pressed = pad.GetButton("Start");
            if (pressed)
            {
                Destroy(Lobby.gameObject);
                Destroy(GameStateManager.gameObject);
                DontDestroyOnLoad(background);
                SceneManager.LoadScene("MainMenu");
                return;
            }
        }
    }
}
