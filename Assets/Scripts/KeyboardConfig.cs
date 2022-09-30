using UnityEngine;

[CreateAssetMenu(fileName = "Keyboard config", menuName = "Keyboard config", order = 2000)]
public class KeyboardConfig : ScriptableObject {

    [Header("Prefabs")]
    [SerializeField] private GameObject keyPrefab;

    [Header("Keys config")]
    [SerializeField] private string[] firstRowKeys;
    [SerializeField] private string[] secondRowKeys;
    [SerializeField] private string[] thirdRowKeys;
    [SerializeField] private string[] fourthRowKeys;

    public GameObject GetKeyPrefab() {
        return keyPrefab;
    }

    public string[] GetFirstRowKeys() {
        return firstRowKeys;
    }

    public string[] GetSecondRowKeys() {
        return secondRowKeys;
    }

    public string[] GetThirdRowKeys() {
        return thirdRowKeys;
    }

    public string[] GetFourthRowKeys() {
        return fourthRowKeys;
    }
}