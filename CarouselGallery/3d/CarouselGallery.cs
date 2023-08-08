using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ListItem
{
    public Sprite image;
    public string text;
}

public class CarouselGallery : MonoBehaviour
{
    public GameObject imageContainer;
    public GameObject imagePrefab;
    public float spacing = 200f;
    public float transitionSpeed = 5f;
    public float swipeThreshold = 50f;

    private GameObject[] imageObjects;
    private int currentIndex = 0;
    private Vector3 targetPosition;
    private Vector2 touchStartPos;

    public List<ListItem> itemList = new List<ListItem>();

    private void Start()
    {
        CreateGallery();
    }

    private void CreateGallery()
    {
        imageObjects = new GameObject[itemList.Count];

        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject imageGO = Instantiate(imagePrefab, imageContainer.transform);
            imageGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemList[i].image;
            imageGO.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = itemList[i].text;

            float xPos = i * spacing;
            imageGO.transform.localPosition = new Vector3(xPos, 0f, 0f);
            imageGO.transform.localScale = Vector3.one;

            imageObjects[i] = imageGO;
        }

        targetPosition = imageContainer.transform.position;
    }

    private void Update()
    {
        // Lerp the container to the target position for a smooth transition
        imageContainer.transform.position = Vector3.Lerp(
            imageContainer.transform.position,
            targetPosition,
            Time.deltaTime * transitionSpeed
        );
    }

    public void MoveRight()
    {
        currentIndex = (currentIndex + 1) % itemList.Count;
        CalculateTargetPosition();
    }

    public void MoveLeft()
    {
        currentIndex = (currentIndex - 1 + itemList.Count) % itemList.Count;
        CalculateTargetPosition();
    }

    public void MoveToIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            currentIndex = index;
            CalculateTargetPosition();
        }
    }

    private void CalculateTargetPosition()
    {
        float newXPos = -currentIndex * spacing;
        targetPosition = new Vector3(
            newXPos,
            imageContainer.transform.position.y,
            imageContainer.transform.position.z
        );
    }
}
