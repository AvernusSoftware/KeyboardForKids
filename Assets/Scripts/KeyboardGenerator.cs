using UnityEngine;

public class KeyboardGenerator : MonoBehaviour {
    [SerializeField] private KeyboardConfig keyboardConfig;
    [SerializeField] private Transform firstRowPanel;
    [SerializeField] private Transform secondRowPanel;
    [SerializeField] private Transform ThirdRowPanel;
    [SerializeField] private Transform FourthRowPanel;

    // Start is called before the first frame update
    private void Start() {
        GenerateKeys(keyboardConfig.GetFirstRowKeys(), firstRowPanel);
        GenerateKeys(keyboardConfig.GetSecondRowKeys(), secondRowPanel);
        GenerateKeys(keyboardConfig.GetThirdRowKeys(), ThirdRowPanel);
        GenerateKeys(keyboardConfig.GetFourthRowKeys(), FourthRowPanel);
    }

    private void GenerateKeys(string[] keys, Transform panel) {
        foreach (string key in keys) {
            GameObject keyPrefab = keyboardConfig.GetKeyPrefab();
            if (keyPrefab == null) {
                continue;
            }

            GameObject keyGameObject = Instantiate(keyPrefab, panel);
            if (keyGameObject == null) {
                continue;
            }

            keyGameObject.name = key;
            KeyObject keyObject = keyGameObject.GetComponent<KeyObject>();
            if (keyObject == null) {
                continue;
            }

            keyObject.Generate(key);
        }
    }
}