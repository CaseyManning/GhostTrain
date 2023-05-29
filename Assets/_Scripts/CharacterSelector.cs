using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    public List<GameObject> characters;

    public GameObject nametext;
    public GameObject desc1;
    public GameObject desc2;

    public RawImage rawIm;

    int current = 0;

    string[] names = { "BRICK", "JUNE", "CHARLIE" };
    string[] desc1s = { "Age: 7", "Age: 9", "Age: 0" };
    string[] desc2s = { "Favorite Ghost:\nSad Ghost", "Likes Frogs", "Covered in Fleas" };

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating = false;

    Quaternion startrot;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < transform.childCount; i++)
        //{
        //    GameObject c = transform.GetChild(i).gameObject;
        //    characters.Add(c);
        //}
        updateChar();

        _sensitivity = 0.4f;
        _rotation = Vector3.zero;

        startrot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        if(Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
        if (_isRotating)
        {
            print("yeah");
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        } else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startrot, 2.5f * Time.deltaTime);
        }
    }

    public void right()
    {
        GetComponent<AudioSource>().Play();
        characters[current].SetActive(false);
        current += 1;
        if (current >= characters.Count)
        {
            current = 0;
        }
        updateChar();
    }

    public void updateChar()
    {
        characters[current].SetActive(true);
        nametext.GetComponent<TMP_Text>().text = names[current];
        desc1.GetComponent<TMP_Text>().text = desc1s[current];
        desc2.GetComponent<TMP_Text>().text = desc2s[current];
    }

    public void left()
    {
        GetComponent<AudioSource>().Play();
        characters[current].SetActive(false);
        current -= 1;
        if (current < 0)
        {
            current = characters.Count-1;
        }
        updateChar();
    }

    public void startgame()
    {
        PlayerScript.character_index = current;
        PlayerScript.character_name = names[current];
        StartCoroutine(fade(0.7f));
    }

    void OnMouseDown()
    {
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    IEnumerator fade(float fadeTime)
    {
        rawIm.gameObject.SetActive(true);
        while (rawIm.color.a < 1)
        {
            Color col = rawIm.color;
            col.a += Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Menu");
    }
}
