using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonobehavior : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private DataManager _dataManager;
    private GameConfig _gameConfig;
    private SceneController _sceneController;
    private GamePlayManager _gamePlayManager;
    private InputHandler _inputHandler;
    private AudioController _audioController;

    public GameManager Gm
    {
        get 
        {
            if(_gameManager == null)
            {
                _gameManager = GameManager.Instance;
            }
            return _gameManager;
        }
    }

    public AudioController Ac
    {
        get
        {
            if (_audioController == null)
            {
                _audioController = AudioController.Instance;
            }
            return _audioController;
        }
    }

    public GamePlayManager Gpm
    {
        get
        {
            if (_gamePlayManager == null)
            {
                _gamePlayManager = GamePlayManager.Instance;
            }
            return _gamePlayManager;
        }
    }
    public UIManager Um
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = UIManager.Instance;
            }
            return _uiManager;
        }
    }

    

    public DataManager Dtm
    {
        get
        {
            if (_dataManager == null)
            {
                _dataManager = DataManager.Instance;
            }
            return _dataManager;
        }
    }

    public GameConfig Gc
    {
        get
        {
            if (_gameConfig == null)
            {
                _gameConfig = GameConfig.Instance;
            }
            return _gameConfig;
        }
    }

    public SceneController Sc
    {
        get
        {
            if (_sceneController == null)
            {
                _sceneController = SceneController.Instance;
            }
            return _sceneController;
        }
    }

    public InputHandler Iph
    {
        get
        {
            if (_inputHandler == null)
            {
                _inputHandler = InputHandler.Instance;
            }
            return _inputHandler;
        }
    }
}
