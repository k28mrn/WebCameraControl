using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCameraController : MonoBehaviour
{
	WebCamTexture webCamTexture;
	int cameraWidth = 1920;
	int cameraHeight = 1080;

	void Start()
	{
			// ウェブカメラの情報を取得
		string webCameraDeviceName = WebCamTexture.devices[0].name;
		
		// ウェブカメラの入力情報をテクスチャーとして取得
		webCamTexture = new WebCamTexture(
			webCameraDeviceName,
			cameraWidth,
			cameraHeight
		);

		// ウェブカメラ起動
		webCamTexture.Play();

		//PCに接続されているウェブカメラデバイスリストを取得
		WebCamDevice[] devices = WebCamTexture.devices;

		// ループで一つづづ確認
		for (int i = 0; i < devices.Length; i++) {
			Debug.Log($"[{i}] device name = {devices[i].name}");
		}

		// ゲームオブジェクトのマテリアルにカメラを表示
		this.GetComponent<Renderer>().material.mainTexture = webCamTexture;
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log($"camera width = {webCamTexture.width}, height = {webCamTexture.height}");
	}
}
