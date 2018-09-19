/**
This work is licensed under a Creative Commons Attribution 3.0 Unported License.
http://creativecommons.org/licenses/by/3.0/deed.en_GB

You are free:

to copy, distribute, display, and perform the work
to make derivative works
to make commercial use of the work
*/

using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GlitchRenderer), PostProcessEvent.AfterStack, "GlitchFX")]
public sealed class GlitchEffect : PostProcessEffectSettings {
	public Texture2D displacementMap;
	//float glitchup, glitchdown, flicker,
	//    glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;

	[Header("Glitch Intensity")]

	[Range(0f, 1f)]
	public FloatParameter intensity = new FloatParameter { value = 0.5f};

	[Range(0f, 1f)]
	public FloatParameter flipIntensity = new FloatParameter { value = 0.5f };

	[Range(0f, 1f)]
	public FloatParameter colorIntensity = new FloatParameter { value = 0.5f };

	public sealed class GlitchRenderer : PostProcessEffectRenderer<GlitchEffect>
	{
		public override void Render(PostProcessRenderContext context)
		{
			//throw new NotImplementedException();

			float glitchup = 0, glitchdown = 0, flicker = 0,
				glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;

			var sheet = context.propertySheets.Get(Shader.Find("Hidden/GlitchShader"));

			sheet.properties.SetFloat("_Intensity", settings.intensity);
			sheet.properties.SetFloat("_ColorIntensity", settings.colorIntensity);
			sheet.properties.SetTexture("_DispTex", settings.displacementMap);

			flicker += Time.deltaTime * settings.colorIntensity;

			if (flicker > flickerTime)
			{
				sheet.properties.SetFloat("filterRadius", UnityEngine.Random.Range(-3f, 3f) * settings.colorIntensity);
				sheet.properties.SetVector("direction", Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360) * settings.colorIntensity, Vector3.forward) * Vector4.one);
				flicker = 0;
				flickerTime = UnityEngine.Random.value;
			}

			if (settings.colorIntensity == 0)
				sheet.properties.SetFloat("filterRadius", 0);

			glitchup += Time.deltaTime * settings.flipIntensity;
			if (glitchup > glitchupTime)
			{
				if (UnityEngine.Random.value < 0.1f * settings.flipIntensity)
					sheet.properties.SetFloat("flip_up", UnityEngine.Random.Range(0, 1f) * settings.flipIntensity);
				else
					sheet.properties.SetFloat("flip_up", 0);

				glitchup = 0;
				glitchupTime = UnityEngine.Random.value / 10f;
			}

			glitchdown += Time.deltaTime * settings.flipIntensity;
			if (glitchdown > glitchdownTime)
			{
				if (UnityEngine.Random.value < 0.1f * settings.flipIntensity)
					sheet.properties.SetFloat("flip_down", 1 - UnityEngine.Random.Range(0, 1f) * settings.flipIntensity);
				else
					sheet.properties.SetFloat("flip_down", 1);

				glitchdown = 0;
				glitchdownTime = UnityEngine.Random.value / 10f;
			}

			if (settings.flipIntensity == 0)
				sheet.properties.SetFloat("flip_down", 1);

			if (UnityEngine.Random.value < 0.05 * settings.intensity)
			{
				sheet.properties.SetFloat("displace", UnityEngine.Random.value * settings.intensity);
				sheet.properties.SetFloat("scale", 1 - UnityEngine.Random.value * settings.intensity);
			}
			else
				sheet.properties.SetFloat("displace", 0);

			context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, -1);
		}
	}

 //   // Called by camera to apply image effect
 //   void OnRenderImage (RenderTexture source, RenderTexture destination) {

	//	material.SetFloat("_Intensity", intensity);
 //       material.SetFloat("_ColorIntensity", colorIntensity);
	//	material.SetTexture("_DispTex", displacementMap);

 //       flicker += Time.deltaTime * colorIntensity;
 //       if (flicker > flickerTime){
	//		material.SetFloat("filterRadius", Random.Range(-3f, 3f) * colorIntensity);
 //           material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
 //           flicker = 0;
	//		flickerTime = Random.value;
	//	}

 //       if (colorIntensity == 0)
 //           material.SetFloat("filterRadius", 0);

 //       glitchup += Time.deltaTime * flipIntensity;
 //       if (glitchup > glitchupTime){
	//		if(Random.value < 0.1f * flipIntensity)
	//			material.SetFloat("flip_up", Random.Range(0, 1f) * flipIntensity);
	//		else
	//			material.SetFloat("flip_up", 0);

	//		glitchup = 0;
	//		glitchupTime = Random.value/10f;
	//	}

 //       if (flipIntensity == 0)
 //           material.SetFloat("flip_up", 0);


 //       glitchdown += Time.deltaTime * flipIntensity;
 //       if (glitchdown > glitchdownTime){
	//		if(Random.value < 0.1f * flipIntensity)
	//			material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * flipIntensity);
	//		else
	//			material.SetFloat("flip_down", 1);

	//		glitchdown = 0;
	//		glitchdownTime = Random.value/10f;
	//	}

 //       if (flipIntensity == 0)
 //           material.SetFloat("flip_down", 1);

 //       if (Random.value < 0.05 * intensity){
	//		material.SetFloat("displace", Random.value * intensity);
	//		material.SetFloat("scale", 1 - Random.value * intensity);
 //       }
 //       else
	//		material.SetFloat("displace", 0);

	//	Graphics.Blit (source, destination, material);
	//}
}
