using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class Analytics : MonoBehaviour {

    private async void Start() {
        try {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        } catch (ConsentCheckException e) {
            Debug.LogWarning(e);
        }
    }
}