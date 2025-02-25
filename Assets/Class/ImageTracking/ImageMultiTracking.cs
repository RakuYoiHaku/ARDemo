using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageMultiTracking : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;

    // 씬에 추가되어있는 게임오브젝트 참조를 위한 리스트 변수
    [SerializeField] private List<GameObject> objectList;

    Dictionary<string, GameObject> _objectDict = new();

    private void Awake()
    {
        foreach (GameObject obj in objectList)
            _objectDict.Add(obj.name, obj);


        // ARTrackedImageManager의 trackedImagesChanged 이벤트가 발생하면,
        // OnImageChanged 함수를 호출한다.
        imageManager.trackedImagesChanged += OnImageChaged;
    }

    private void OnImageChaged(ARTrackedImagesChangedEventArgs args)
    {

        // 인식을 완료한 이미지가 생겼을때,
        foreach (var trackedImage in args.added)
        {
            RefreshObject(trackedImage);
        }

        // 이미 인식한 이미지의 정보를 갱신했을때,
        foreach (var trackedImage in args.updated)
        {
            RefreshObject(trackedImage);
        }
    }

    private void RefreshObject(ARTrackedImage image)
    {
        // ARTrackedImage의 이름을 가져옴
        string imageName = image.referenceImage.name;

        // 이미지의 이름을 이용해 딕셔너리에 저장된 게임오브젝트를 참조
        GameObject obj = _objectDict[imageName];

        // 게임오브젝트를 활성화
        obj.SetActive(true);

        // 인식한 이미지의 위치와 회전값으로 게임오브젝트의 위치와 회전값을 맞춤
        obj.transform.position = image.transform.position;
        obj.transform.rotation = image.transform.rotation;
    }
}
