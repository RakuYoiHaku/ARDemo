using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageMultiTracking : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;

    // ���� �߰��Ǿ��ִ� ���ӿ�����Ʈ ������ ���� ����Ʈ ����
    [SerializeField] private List<GameObject> objectList;

    Dictionary<string, GameObject> _objectDict = new();

    private void Awake()
    {
        foreach (GameObject obj in objectList)
            _objectDict.Add(obj.name, obj);


        // ARTrackedImageManager�� trackedImagesChanged �̺�Ʈ�� �߻��ϸ�,
        // OnImageChanged �Լ��� ȣ���Ѵ�.
        imageManager.trackedImagesChanged += OnImageChaged;
    }

    private void OnImageChaged(ARTrackedImagesChangedEventArgs args)
    {

        // �ν��� �Ϸ��� �̹����� ��������,
        foreach (var trackedImage in args.added)
        {
            RefreshObject(trackedImage);
        }

        // �̹� �ν��� �̹����� ������ ����������,
        foreach (var trackedImage in args.updated)
        {
            RefreshObject(trackedImage);
        }
    }

    private void RefreshObject(ARTrackedImage image)
    {
        // ARTrackedImage�� �̸��� ������
        string imageName = image.referenceImage.name;

        // �̹����� �̸��� �̿��� ��ųʸ��� ����� ���ӿ�����Ʈ�� ����
        GameObject obj = _objectDict[imageName];

        // ���ӿ�����Ʈ�� Ȱ��ȭ
        obj.SetActive(true);

        // �ν��� �̹����� ��ġ�� ȸ�������� ���ӿ�����Ʈ�� ��ġ�� ȸ������ ����
        obj.transform.position = image.transform.position;
        obj.transform.rotation = image.transform.rotation;
    }
}
