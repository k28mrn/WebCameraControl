using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCameraEffect : MonoBehaviour
{
	public RawImage rawImage; // webカメラの情報を表示するためのRawImage
	WebCamTexture webCamTexture; //webカメラにアクセスするためのインスタンス
	Color32[] colors; // webカメラで表示されている色情報を格納するための配列
	Texture2D texture; //プレーンに表示するようのテクスチャー
	int cameraWidth = 1280; //カメラの幅
	int cameraHeight = 720; //カメラの高さ
	
	int type = 2;

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

		//エフェクト用テクスチャー
		texture = new Texture2D(
			cameraWidth,
			cameraHeight,
			TextureFormat.RGBA32,
			false
		);

		//RawImageにテクスチャーを設定
		rawImage.texture = texture;

		//配列に格納できる大きさを設定カメラの面積分
		colors = new Color32[cameraWidth * cameraHeight];
	}

	void Update()
	{
		//カメラが起動する前は16なのでその場合は実行しない
		if (webCamTexture.width > 16) { 

			//写真pixel色情報をcolorsに格納
			webCamTexture.GetPixels32(colors);

			//テクスチャー情報を1pxづつ全てループで回す
			for (int x = 0; x < cameraWidth; x++) { //テクスチャーの幅分ループ
				for (int y = 0; y < cameraHeight; y++) { //テクスチャーの高さ分ループ
					int num = x + y * cameraWidth;
					Color32 c = colors[num];

					//ここでエフェクト処理
					// start -----------------
					if (type == 0) { //反転
						c.r = (byte)(255.0f - c.r);
						c.g = (byte)(255.0f - c.g);
						c.b = (byte)(255.0f - c.b);
					} else if (type == 1) { //グレイスケール
						byte gray = (byte)(c.r * 0.2f + c.g * 0.7f + c.b * 0.07f);
						c.r = gray;
						c.g = gray;
						c.b = gray;
					} else if (type == 2) { // ノイズ
						float r = Random.Range(0.0f, 10.0f);
						if (r <= 3.0f) {
							c.r = (byte)(Random.Range(0.0f, 255.0f));
							c.g = (byte)(Random.Range(0.0f, 255.0f));
							c.b = (byte)(Random.Range(0.0f, 255.0f));
						}
					}
					// end --------------------
					colors[num] = c;
				}
			}
			//テクスチャーに色情報を適応
			texture.SetPixels32(colors);
			texture.Apply();
		}
	}
}
