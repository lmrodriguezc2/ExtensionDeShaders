using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Batería : MonoBehaviour
{
	private GameObject Crash;
	private GameObject Ride;
	private GameObject PlatoHh1;
	private GameObject PlatoHh2;
	
	private Renderer renderPlato;
	public Texture2D imgPlato;
	
	private Renderer renderMetal;
	public Texture2D imgMetal;
	
	private Renderer renderPiso;
	public Texture2D imgPiso;
	
	private Renderer renderPared;
	public Texture2D imgPared;
	Shader shader1;
	
	private Renderer renderTambor;
	public Texture2D imgTambor;
	
	public int cantidadPorNota = 50;
	GameObject objetoBase;
	GameObject objetoBase1;
	bool lugarBool = true;
	bool boolHi = true;
	bool golpeBool = true;
	bool blueL = false;
	bool redL = false;
	float deltax = 0.01f;
	float deltay;
	float sen1;
	float sen2 = 0;
	float transformation = 1;
	float actualx = 0;
	Vector3 mov;
	GameObject lightGameObject1;
	Light lightComp1;
	
	AnimationCurve anim;
    Keyframe[] ks;

	// Start is called before the first frame update
	void Start()
	{
		lightGameObject1 = new GameObject("BlueL");
		lightComp1 = lightGameObject1.AddComponent<Light>();
		lightGameObject1.transform.position = new Vector3(6.319f, 3.855f, 0.2070002f);
		lightGameObject1.transform.rotation *= Quaternion.Euler(27.005f, -91.675f, -78.708f);
		lightComp1.type = LightType.Spot;
		lightComp1.intensity = 300f;
		lightComp1.shadows = LightShadows.Soft;
		lightGameObject1.SetActive(false);
		shader1 = Shader.Find("Geometry/TrianglePyramidExtrusion");

		
		GameObject lightGameObject = new GameObject("The Light");
		Light lightComp = lightGameObject.AddComponent<Light>();
		lightGameObject.transform.position = new Vector3(6.35f, 4.58f, 2.44f);
		lightGameObject.transform.rotation *= Quaternion.Euler(21.563f, -104.491f, -52.945f);
		lightComp.type = LightType.Directional;
		lightComp.intensity = 1f;
		lightComp.shadows = LightShadows.Soft;

	
		
		objetoBase = GameObject.Find("Base");
		objetoBase1 = GameObject.Find("Base2");
		for(int i = 0; i < cantidadPorNota; i++)
        {
			GameObject nt = MakeNota1(i);
			nt.transform.Translate(notColliding());
			//nt.transform.Rotate(0.0f, Random.Range(0.0f, 359.0f), 0.0f, Space.Self);
			nt.transform.parent = objetoBase.transform;
		}
		for (int i = 0; i < cantidadPorNota; i++)
		{
			GameObject nt = MakeNota2(i);
			nt.transform.Translate(notColliding());
			//nt.transform.Rotate(0.0f, Random.Range(0.0f, 359.0f), 0.0f, Space.Self);
			nt.transform.parent = objetoBase.transform;
		}
		
		GameObject coso = MakePersonita();
		GameObject nubes = MakeNubes();
		
		coso.transform.Translate(new Vector3(5.14f, -1.6023916f, -19.00562f));
		coso.transform.parent = objetoBase1.transform;
		
		nubes.transform.Translate(new Vector3(4.8f, -0.13f, -17.7f));
		nubes.transform.parent = objetoBase1.transform;
		
		hacerBateria();
		
	}
	
	private void cambioLugar(Transform Nota)
	{
		if(lugarBool == true)
		{
			Nota.Translate(Vector3.up);
		}
		else
		{
			Nota.Translate(Vector3.down);
		}	
	}
	
	private void cambioColor(GameObject Nota)
	{
		if(Nota.GetComponent<Renderer>().material.color == Color.red)
		{
			Nota.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
			
		}
		else
		{
			Nota.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			
		}	
	}
	
	
	void Update()
    {
		if(blueL == true){
			lightGameObject1.SetActive(true);
			lightComp1.color = Color.blue;
		}
		if(redL == true){
			lightGameObject1.SetActive(true);
			lightComp1.color = Color.red;
		}
		
		GameObject objetoBase = GameObject.Find("Base");
		if(golpeBool == true){
			for(var con = -20; con <21; con++){
				Crash.transform.Rotate(con*0.1f, 0.09f, con*0.1f,relativeTo:Space.Self); 
				if(boolHi == true){
				PlatoHh2.transform.Translate(0, 0.05f, 0, Space.Self);
				boolHi = false;
				}
				else{
					PlatoHh2.transform.Translate(0, -0.05f, 0, Space.Self);
					boolHi = true;
				}
				Ride.transform.Rotate(con*0.1f, 0.09f, con*0.1f,relativeTo:Space.Self);
			}
		}
		foreach (Transform child in objetoBase.transform)
		{
			sen1 = sen2;
			sen2 = Mathf.Sin(Mathf.Deg2Rad * actualx * transformation);
			mov = new Vector3(deltax, (sen2 - sen1) * 8.0f, 0);
			child.Translate(mov);
			child.position = outOfBounds(child.position);
			actualx += deltax * 8;
			if (actualx == 360)
				actualx = 0;
		}
		 if (Input.GetButtonDown("Fire1"))
		{
			lugarBool = !lugarBool;
			for(int i =0; i<objetoBase.transform.childCount; i++){
				cambioLugar(objetoBase.transform.GetChild(i).transform);
				for(int j =0; j<objetoBase.transform.GetChild(i).childCount; j++){
					cambioColor(objetoBase.transform.GetChild(i).GetChild(j).gameObject);
				}
			}
		}
		if (Input.GetKeyDown("space"))
		{
			if(golpeBool == true){
				golpeBool = false;
			}
			else{
				golpeBool = true;
			}
				
		}
		
		if (Input.GetKeyDown("up"))
		{
			if(blueL == false && redL == false){
				blueL = true;
			}
			else{
				if(redL == false && blueL == true){
					redL = true;
					blueL = false;
				}
				
			else{
					blueL = true;
					redL = false;
					
				}
				
			}
				
		}
		if (Input.GetKeyDown("down"))
		{
			blueL = false;
			redL = false;
			lightGameObject1.SetActive(false);
				
		}
    }
	
	GameObject MakePersonita()
	{
		GameObject Persona = new GameObject("Cabeza");
		GameObject Capsula1 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Capsula1.transform.SetParent(Persona.transform);
		Capsula1.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		Capsula1.transform.localScale = new Vector3(1, 0.786612f, 1.2022f);
		Capsula1.GetComponent<Renderer>().material.color = Color.white;
		
		GameObject Capsula2 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Capsula2.transform.SetParent(Persona.transform);
		Capsula2.transform.localPosition = new Vector3(0.46f, 0.1143916f, -0.20738f);
		Capsula2.transform.localScale = new Vector3(0.2659076f, 0.1835879f, 0.1484642f);
		Capsula2.GetComponent<Renderer>().material.color = Color.black;
		
		GameObject Capsula3 = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		Capsula3.transform.SetParent(Persona.transform);
		Capsula3.transform.localPosition = new Vector3(0.46f, 0.1143916f, 0.27462f);
		Capsula3.transform.localScale = new Vector3(0.2659076f, 0.1835879f, 0.1484642f);
		Capsula3.GetComponent<Renderer>().material.color = Color.black;
		
		GameObject Capsula4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		Capsula4.transform.SetParent(Persona.transform);
		Capsula4.transform.localPosition = new Vector3(0.015f, -0.7576084f, -0.03438f);
		Capsula4.transform.localScale = new Vector3(0.39745f, 0.39745f, 0.39745f);
		Capsula4.GetComponent<Renderer>().material.color = Color.white;
		
		GameObject Capsula5 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		Capsula5.transform.SetParent(Persona.transform);
		Capsula5.transform.localPosition = new Vector3(0.014f, 0.0953916f, 0.65262f);
		Capsula5.transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
		Capsula5.transform.localScale = new Vector3(0.4208039f, 0.07801241f, 0.4208039f);
		Capsula5.GetComponent<Renderer>().material.color = Color.red;
		
		GameObject Capsula6 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		Capsula6.transform.SetParent(Persona.transform);
		Capsula6.transform.localPosition = new Vector3(0.014f, 0.0953916f, -0.63038f);
		Capsula6.transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
		Capsula6.transform.localScale = new Vector3(0.4208039f, 0.07801241f, 0.4208039f);
		Capsula6.GetComponent<Renderer>().material.color = Color.red;
		
		GameObject Capsula7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Capsula7.transform.SetParent(Persona.transform);
		Capsula7.transform.localPosition = new Vector3(0.004f, 0.4873916f, 0.66362f);
		Capsula7.transform.localScale = new Vector3(0.1111604f, 0.76842f, 0.076448f);
		Capsula7.GetComponent<Renderer>().material.color = Color.red;
		
		GameObject Capsula8 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Capsula8.transform.SetParent(Persona.transform);
		Capsula8.transform.localPosition = new Vector3(-0.004f, 0.8363916f, 0.01062f);	
		Capsula8.transform.rotation *= Quaternion.Euler(90.0f, 0, 0);
		Capsula8.transform.localScale = new Vector3(0.1111604f, 1.2787f, 0.076448f);
		Capsula8.GetComponent<Renderer>().material.color = Color.red;
		
		GameObject Capsula9 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Capsula9.transform.SetParent(Persona.transform);
		Capsula9.transform.localPosition = new Vector3(0.004f, 0.4873916f, -0.65138f);
		Capsula9.transform.localScale = new Vector3(0.1111604f, 0.76842f, 0.076448f);
		Capsula9.GetComponent<Renderer>().material.color = Color.red;
		
		return Persona;
	}
	
	GameObject MakeNubes()
	{
		GameObject Nube = new GameObject("Nube");
		GameObject Esfera1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Esfera1.transform.SetParent(Nube.transform);
		Esfera1.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		Esfera1.transform.localScale = new Vector3(0.3618364f, 0.3618364f, 0.3618364f);
		Esfera1.GetComponent<Renderer>().material.color = Color.white;
		
		GameObject Esfera2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Esfera2.transform.SetParent(Nube.transform);
		Esfera2.transform.localPosition = new Vector3(0.0f, 0.43f, 0.51f);
		Esfera2.transform.localScale = new Vector3(0.5106958f, 0.5106958f, 0.5106958f);
		Esfera2.GetComponent<Renderer>().material.color = Color.white;
		
		GameObject Esfera3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Esfera3.transform.SetParent(Nube.transform);
		Esfera3.transform.localPosition = new Vector3(0.0f, 1.0f, 1.2f);
		Esfera3.transform.localScale = new Vector3(0.7182937f, 0.7182937f, 0.7182937f);
		Esfera3.GetComponent<Renderer>().material.color = Color.white;
		
		GameObject Esfera4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Esfera4.transform.SetParent(Nube.transform);
		Esfera4.transform.localPosition = new Vector3(0.0f, 1.54f, 2.42f);
		Esfera4.transform.localScale = new Vector3(1.776559f, 1.776559f, 1.776559f);
		Esfera4.GetComponent<Renderer>().material.color = Color.white;
		
		return Nube;
	}
	
	GameObject MakeNota1(int numero)
	{
		GameObject Nota1 = new GameObject("NotaG1-" + numero);
		GameObject Nota10 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota10.transform.SetParent(Nota1.transform);
		Nota10.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		Nota10.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota10.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota11 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota11.transform.SetParent(Nota1.transform);
		Nota11.transform.position = new Vector3(0.0f, 0.0f, 0.076f);
		Nota11.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota11.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota12 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota12.transform.SetParent(Nota1.transform);
		Nota12.transform.position = new Vector3(0.0f, 0.046f, 0.0384f);
		Nota12.transform.rotation *= Quaternion.Euler(90, -1.525879e-05f, -1.525879e-05f);
		Nota12.transform.localScale = new Vector3(0.01288273f, 0.08640555f, 0.01065468f);
		Nota12.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota13 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota13.transform.SetParent(Nota1.transform);
		Nota13.transform.position = new Vector3(0.0026688f, -0.0587f, -0.0091f);
		Nota13.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota13.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota13.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota14 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota14.transform.SetParent(Nota1.transform);
		Nota14.transform.position = new Vector3(0.0037688f, -0.058f, 0.064f);
		Nota14.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota14.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota14.GetComponent<Renderer>().material.color = Color.blue;

		return Nota1;
	}

	GameObject MakeNota2(int numero)
	{
		GameObject Nota2 = new GameObject("NotaG2-" + numero);

		GameObject Nota20 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota20.transform.SetParent(Nota2.transform);
		Nota20.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		Nota20.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota20.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota21 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota21.transform.SetParent(Nota2.transform);
		Nota21.transform.position = new Vector3(-0.00000001f, 0.0402f, 0.0171f);
		Nota21.transform.rotation *= Quaternion.Euler(101.614f, -1.525879e-05f, -1.525879e-05f);
		Nota21.transform.localScale = new Vector3(0.01288273f, 0.04592196f, 0.01065468f);
		Nota21.GetComponent<Renderer>().material.color = Color.blue;

		GameObject Nota22 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota22.transform.SetParent(Nota2.transform);
		Nota22.transform.position = new Vector3(0.0026688f, -0.0587f, -0.0091f);
		Nota22.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota22.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota22.GetComponent<Renderer>().material.color = Color.blue;

		return Nota2;
	}

	Vector3 notColliding()
    {
		float x = Random.Range(-3.4f, 3.1f);
		float y = Random.Range(0.0f, 3.0f);
		float z = Random.Range(-3.7f, 3.6f);
		while (x < 1.4f && x > -0.5f && y < 1.4f && y > 0.0f && z < 1.2f && z > -1.0f)
        {
			x = Random.Range(-3.4f, 3.1f);
			y = Random.Range(0.0f, 3.0f);
			z = Random.Range(-3.7f, 3.6f);
		}
		return new Vector3(x, y, z);
	}
	
	Vector3 outOfBounds(Vector3 inp)
    {
		if (inp.x < -3.4f)
			inp.x = 3.1f;
		if (inp.x > 3.1f)
			inp.x = -3.4f;
		if (inp.z < -3.7f)
			inp.z = 3.6f;
		if (inp.z > 3.6f)
			inp.z = -3.7f;
		return inp;
	}
	
    void hacerBateria()
    {
		//Texture2D plato = (Texture2D) Resources.Load ("platos");
		//Texture textura = (Texture) Resources.Load("platos");
        //Material plato = new Material(Shader.Find("VertexLit"));
        //plato.SetTexture("_MainTex", textura);
		
		
		Color colorPiso = new Color(0.7058823529411765f, 0.5019607843137255f, 0.3176470588235294f, 0.50f);
        GameObject piso = GameObject.CreatePrimitive(PrimitiveType.Plane);
        piso.transform.position = new Vector3(0, 0, 0);
		piso.transform.localScale = new Vector3 (0.8753638f, 0.8753638f, 0.8753638f);
		//piso.GetComponent<Renderer>().material.color = colorPiso;
		renderPiso = piso.GetComponent<Renderer>();
		renderPiso.material.SetTexture("_MainTex", imgPiso);
		renderPiso.material.SetTextureScale("_MainTex", new Vector2(30,30));
		//renderPiso.material.SetTextureOffset("_MainTex", new Vector2(1.5f, 1));
		
		GameObject piso1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        piso1.transform.position = new Vector3(-3.519999f, 0, 3.75f);
		piso1.transform.rotation *= Quaternion.Euler(0, 90, -90);
		piso1.transform.localScale = new Vector3 (1.404084f, 0.8753645f, 1.373271f);
		//piso1.GetComponent<Renderer>().material.color = Color.red;
		renderPared = piso1.GetComponent<Renderer>();
		renderPared.material.SetTexture("_MainTex", imgPared);
		renderPared.material.shader = shader1;
		renderPared.material.SetTextureScale("_MainTex", new Vector2(10,10));

		for (int i = 0; i < 10; i++)
		{
			for(int j = 0; j < 5; j++)
            {
				var noise = Mathf.PerlinNoise(-2 + i * 0.25f, j * 0.25f);
				GameObject soundS = GameObject.CreatePrimitive(PrimitiveType.Cube);
				soundS.transform.localPosition = new Vector3(-2 + i * 0.25f, 1 + j*0.25f, noise + 3.3f);
				soundS.transform.localScale = new Vector3(0.25f, 0.25f, 1.25f);
				soundS.transform.parent = piso1.transform;
			}
		}
		
		GameObject piso2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        piso2.transform.position = new Vector3(-3.519999f, 0, -3.82f);
		piso2.transform.rotation *= Quaternion.Euler(0, 90, 90);
		piso2.transform.localScale = new Vector3 (1.536353f, 0.8753645f, 1.493284f);
		//piso2.GetComponent<Renderer>().material.color = Color.red;
		renderPared = piso2.GetComponent<Renderer>();
		renderPared.material.shader = shader1;
		renderPared.material.SetTexture("_MainTex", imgPared);
		renderPared.material.SetTextureScale("_MainTex", new Vector2(10,10));

		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				var noise = Mathf.PerlinNoise(-2 + i * 0.25f - 3.6f, j * 0.25f - 3.6f);
				GameObject soundS = GameObject.CreatePrimitive(PrimitiveType.Cube);
				soundS.transform.localPosition = new Vector3(-2 + i * 0.25f, 1 + j * 0.25f, noise + -4.2f);
				soundS.transform.localScale = new Vector3(0.25f, 0.25f, 1.25f);
				soundS.transform.parent = piso2.transform;
			}
		}

		GameObject piso3 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        piso3.transform.position = new Vector3(-3.519999f, 0, -0.01f);
		piso3.transform.rotation *= Quaternion.Euler(0, 0, -90);
		piso3.transform.localScale = new Vector3 (1.632347f, 0.8753645f, 0.8753637f);
		//piso3.GetComponent<Renderer>().material.color = Color.red;
		renderPared = piso3.GetComponent<Renderer>();
		renderPared.material.shader = shader1;
		renderPared.material.SetTexture("_MainTex", imgPared);
		renderPared.material.SetTextureScale("_MainTex", new Vector2(10, 10));

		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				var noise = Mathf.PerlinNoise(1 + j * 0.25f, -1 + i * 0.25f);
				GameObject soundS = GameObject.CreatePrimitive(PrimitiveType.Cube);
				soundS.transform.localPosition = new Vector3(noise + -4.0f, 1 + j * 0.25f, -1 + i * 0.25f);
				soundS.transform.localScale = new Vector3(1.25f, 0.25f, 0.25f);
				soundS.transform.parent = piso3.transform;
			}
		}

		GameObject Bombo1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Bombo1.transform.position = new Vector3(2.6358e-10f, 0.517f, -2.7425e-09f);
		Bombo1.transform.rotation *= Quaternion.Euler(0, 0, -90.43301f);
		Bombo1.transform.localScale = new Vector3 (0.97699f, 0.4058026f, 0.97699f);
		//Bombo1.GetComponent<Renderer>().material.color = Color.black;
		renderTambor = Bombo1.GetComponent<Renderer>();
		renderTambor.material.SetTexture("_MainTex", imgTambor);
		
		GameObject Bombo2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Bombo2.transform.position = new Vector3(0.38f, 0.515f, 4.3221e-09f);
		Bombo2.transform.rotation *= Quaternion.Euler(0, 0, -90.43301f);
		Bombo2.transform.localScale = new Vector3 (1.046552f, 0.05190701f, 1.046552f);
		//Bombo2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = Bombo2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject Bombo3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Bombo3.transform.position = new Vector3(-0.361f, 0.52f, 4.3221e-09f);
		Bombo3.transform.rotation *= Quaternion.Euler(0, 0, -90.43301f);
		Bombo3.transform.localScale = new Vector3 (1.046552f, 0.05190701f, 1.046552f);
		//Bombo3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = Bombo3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject TomP1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomP1.transform.position = new Vector3(0.893f, 0.543f, 0.742f);
		TomP1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		TomP1.transform.localScale = new Vector3 (0.5435523f, 0.2738092f, 0.5852428f);
		//TomP1.GetComponent<Renderer>().material.color = Color.black;
		renderTambor = TomP1.GetComponent<Renderer>();
		renderTambor.material.SetTexture("_MainTex", imgTambor);
		
		GameObject TomP2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomP2.transform.position = new Vector3(0.893f, 0.823f, 0.742f);
		TomP2.transform.rotation *= Quaternion.Euler(0, 0, 0);
		TomP2.transform.localScale = new Vector3 (0.590051f, 0.02693849f, 0.6353079f);
		//TomP2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomP2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject TomP3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomP3.transform.position = new Vector3(0.893f, 0.266f, 0.742f);
		TomP3.transform.rotation *= Quaternion.Euler(0, 0, 0);
		TomP3.transform.localScale = new Vector3 (0.590051f, 0.02693849f, 0.6353079f);
		//TomP3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomP3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject Redo1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Redo1.transform.position = new Vector3(1.118f, 0.76244f, -0.174f);
		Redo1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		Redo1.transform.localScale = new Vector3 (0.5581056f, 0.02260188f, 0.6009123f);
		//Redo1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = Redo1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject Redo2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Redo2.transform.position = new Vector3(1.118f, 0.83811f, -0.174f);
		Redo2.transform.rotation *= Quaternion.Euler(0, 0, 0);
		Redo2.transform.localScale = new Vector3 (0.5141245f, 0.09110307f, 0.5535578f);
		//Redo2.GetComponent<Renderer>().material.color = Color.black;
		renderTambor = Redo2.GetComponent<Renderer>();
		renderTambor.material.SetTexture("_MainTex", imgTambor);
		
		GameObject Redo3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Redo3.transform.position = new Vector3(1.118f, 0.91756f, -0.174f);
		Redo3.transform.rotation *= Quaternion.Euler(0, 0, 0);
		Redo3.transform.localScale = new Vector3 (0.5581056f, 0.02260188f, 0.6009123f);
		//Redo3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = Redo3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject TomA1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomA1.transform.position = new Vector3(0.1180801f, 1.205066f, 0.3503315f);
		TomA1.transform.rotation *= Quaternion.Euler(-6.536f, 13.682f, -67.993f);
		TomA1.transform.localScale = new Vector3 (0.4494038f, 0.1625813f, 0.4838731f);
		//TomA1.GetComponent<Renderer>().material.color = Color.black;
		renderTambor = TomA1.GetComponent<Renderer>();
		renderTambor.material.SetTexture("_MainTex", imgTambor);
		
		GameObject TomA2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomA2.transform.position = new Vector3(0.2661705f, 1.266954f, 0.3069614f);
		TomA2.transform.rotation *= Quaternion.Euler(-6.536f, 13.682f, -67.993f);
		TomA2.transform.localScale = new Vector3 (0.4878485f, 0.01599543f, 0.5252665f);
		//TomA2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomA2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject TomA3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomA3.transform.position = new Vector3(-0.02838556f, 1.14386f, 0.3932028f);
		TomA3.transform.rotation *= Quaternion.Euler(-6.536f, 13.682f, -67.993f);
		TomA3.transform.localScale = new Vector3 (0.4878485f, 0.01599543f, 0.5252665f);
		//TomA3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomA3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject TomF1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomF1.transform.position = new Vector3(0.1159914f, 1.219998f, -0.2669589f);
		TomF1.transform.rotation *= Quaternion.Euler(2.678f, -11.112f, -70.11f);
		TomF1.transform.localScale = new Vector3 (0.4077275f, 0.1475041f, 0.4390002f);
		//TomF1.GetComponent<Renderer>().material.color = Color.black;
		renderTambor = TomF1.GetComponent<Renderer>();
		renderTambor.material.SetTexture("_MainTex", imgTambor);
		
		GameObject TomF2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomF2.transform.position = new Vector3(0.2547183f, 1.271318f, -0.23728f);
		TomF2.transform.rotation *= Quaternion.Euler(2.678f, -11.112f, -70.11f);
		TomF2.transform.localScale = new Vector3 (0.4426069f, 0.01451207f, 0.4765549f);
		//TomF2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomF2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		
		GameObject TomF3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TomF3.transform.position = new Vector3(-0.02121226f, 1.169282f, -0.29633f);
		TomF3.transform.rotation *= Quaternion.Euler(2.678f, -11.112f, -70.11f);
		TomF3.transform.localScale = new Vector3 (0.4426069f, 0.01451207f, 0.4765549f);
		//TomF3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = TomF3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP1.transform.position = new Vector3(0.907f, 0.473f, 1.053f);
		BaseTomP1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		BaseTomP1.transform.localScale = new Vector3 (0.02455554f, 0.2847003f, 0.02455554f);
		//BaseTomP1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP12 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP12.transform.position = new Vector3(0.9080001f, 0.152f, 1.079f);
		BaseTomP12.transform.rotation *= Quaternion.Euler(-32.247f, 0, 0);
		BaseTomP12.transform.localScale = new Vector3 (0.02455554f, 0.05328071f, 0.02455554f);
		//BaseTomP12.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP12.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP13 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP13.transform.position = new Vector3(0.907f, 0.057f, 1.105f);
		BaseTomP13.transform.rotation *= Quaternion.Euler(0, 0, 0);
		BaseTomP13.transform.localScale = new Vector3 (0.02455554f, 0.05687318f, 0.02455554f);
		//BaseTomP13.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP13.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP2.transform.position = new Vector3(1.107f, 0.473f, 0.5349999f);
		BaseTomP2.transform.rotation *= Quaternion.Euler(0, 0, 0);
		BaseTomP2.transform.localScale = new Vector3 (0.02455554f, 0.2847003f, 0.02455554f);
		//BaseTomP2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP22 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP22.transform.position = new Vector3(1.1216f, 0.1495f, 0.5142f);
		BaseTomP22.transform.rotation *= Quaternion.Euler(30.996f, -30.651f, 2.107f);
		BaseTomP22.transform.localScale = new Vector3 (0.02455554f, 0.05328071f, 0.02455554f);
		//BaseTomP22.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP22.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP23 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP23.transform.position = new Vector3(1.1358f, 0.0547f, 0.4927999f);
		BaseTomP23.transform.rotation *= Quaternion.Euler(0.043f, -41.399f, -1.674f);
		BaseTomP23.transform.localScale = new Vector3 (0.02455554f, 0.05687318f, 0.02455554f);
		//BaseTomP23.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP23.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP3.transform.position = new Vector3(0.629f, 0.473f, 0.6039999f);
		BaseTomP3.transform.rotation *= Quaternion.Euler(0, 0, 0);
		BaseTomP3.transform.localScale = new Vector3 (0.02455554f, 0.2847003f, 0.02455554f);
		//BaseTomP3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP32 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP32.transform.position = new Vector3(0.606f, 0.151f, 0.5919999f);
		BaseTomP32.transform.rotation *= Quaternion.Euler(30.042f, -308.597f, -6.37f);
		BaseTomP32.transform.localScale = new Vector3 (0.02455554f, 0.05328071f, 0.02455554f);
		//BaseTomP32.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP32.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomP33 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomP33.transform.position = new Vector3(0.7530001f, 0.05800003f, 0.9019999f);
		BaseTomP33.transform.rotation *= Quaternion.Euler(8.629001f, -320.435f, -11.213f);
		BaseTomP33.transform.localScale = new Vector3 (0.02455554f, 0.05687318f, 0.02455554f);
		//BaseTomP33.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomP33.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo1.transform.position = new Vector3(1.102f, 0.414f, -0.171f);
		BaseRedo1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		BaseRedo1.transform.localScale = new Vector3 (0.04012181f, 0.1966915f, 0.04012181f);
		//BaseRedo1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo2.transform.position = new Vector3(1.102f, 0.691f, -0.04299998f);
		BaseRedo2.transform.rotation *= Quaternion.Euler(54.699f, 0, 0);
		BaseRedo2.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo3.transform.position = new Vector3(1.034f, 0.685f, -0.272f);
		BaseRedo3.transform.rotation *= Quaternion.Euler(-56.179f, 20.004f, 11.093f);
		BaseRedo3.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo4.transform.position = new Vector3(1.234f, 0.689f, -0.171f);
		BaseRedo4.transform.rotation *= Quaternion.Euler(-35.014f, -27.087f, -226.862f);
		BaseRedo4.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo4.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo4.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo5 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo5.transform.position = new Vector3(1.1f, 0.101f, -0.271f);
		BaseRedo5.transform.rotation *= Quaternion.Euler(42.192f, 0, 0);
		BaseRedo5.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo5.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo5.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo6 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo6.transform.position = new Vector3(1.163f, 0.08700001f, -0.08599997f);
		BaseRedo6.transform.rotation *= Quaternion.Euler(-38.328f, 24.37f, 7.846f);
		BaseRedo6.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo6.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo6.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseRedo7 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseRedo7.transform.position = new Vector3(0.993f, 0.105f, -0.139f);
		BaseRedo7.transform.rotation *= Quaternion.Euler(-33.595f, -27.97f, -206.944f);
		BaseRedo7.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseRedo7.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseRedo7.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomA1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomA1.transform.position = new Vector3(0.15f, 1.151f, 0.152f);
		BaseTomA1.transform.rotation *= Quaternion.Euler(54.699f, 0, 0);
		BaseTomA1.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseTomA1.GetComponent<Renderer>().material.color = Color.grey;0
		renderMetal = BaseTomA1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomA2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomA2.transform.position = new Vector3(0.15f, 1.155f, -0.101f);
		BaseTomA2.transform.rotation *= Quaternion.Euler(-51.037f, 0, 0);
		BaseTomA2.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseTomA2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomA2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseTomA3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseTomA3.transform.position = new Vector3(0.15f, 0.89f, 0.026f);
		BaseTomA3.transform.rotation *= Quaternion.Euler(-1.875f, 0, 0);
		BaseTomA3.transform.localScale = new Vector3 (0.04012181f, 0.1625675f, 0.04012181f);
		//BaseTomA3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseTomA3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseC1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseC1.transform.position = new Vector3(0.456f, 0.698f, -0.834f);
		BaseC1.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 1.704f);
		BaseC1.transform.localScale = new Vector3 (0.04012181f, 0.4673834f, 0.04012181f);
		//BaseC1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseC1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseC2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseC2.transform.position = new Vector3(0.487f, 0.135f, -0.97f);
		BaseC2.transform.rotation *= Quaternion.Euler(43.802f, -0.033f, 5.622f);
		BaseC2.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseC2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseC2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseC3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseC3.transform.position = new Vector3(0.544f, 0.135f, -0.748f);
		BaseC3.transform.rotation *= Quaternion.Euler(-38.278f, 17.012f, 13.473f);
		BaseC3.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseC3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseC3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseC4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseC4.transform.position = new Vector3(0.361f, 0.138f, -0.818f);
		BaseC4.transform.rotation *= Quaternion.Euler(-30.11f, -33.526f, -203.751f);
		BaseC4.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseC4.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseC4.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		Crash = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Crash.transform.position = new Vector3(0.451f, 1.111f, -0.8199999f);
		Crash.transform.rotation *= Quaternion.Euler(10.807f, -3.012f, -23.988f);
		Crash.transform.localScale = new Vector3 (0.7209054f, 0.007338614f, 0.7209054f);
		//Crash.GetComponent<Renderer>().material = plato;
		renderPlato = Crash.GetComponent<Renderer>();
		renderPlato.material.SetTexture("_MainTex", imgPlato);
		//Crash.GetComponent<Renderer>().material.color = Color.yellow;
		
		GameObject BaseHh1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseHh1.transform.position = new Vector3(1.165f, 0.609f, -0.739f);
		BaseHh1.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 1.704f);
		BaseHh1.transform.localScale = new Vector3 (0.04052505f, 0.6276549f, 0.04052505f);
		//BaseHh1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseHh1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseHh2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseHh2.transform.position = new Vector3(1.1938f, 0.12769f, -0.87397f);
		BaseHh2.transform.rotation *= Quaternion.Euler(43.802f, -0.033f, 5.622f);
		BaseHh2.transform.localScale = new Vector3 (0.04052505f, 0.1917871f, 0.04052505f);
		//BaseHh2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseHh2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseHh3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseHh3.transform.position = new Vector3(1.2515f, 0.12769f, -0.64973f);
		BaseHh3.transform.rotation *= Quaternion.Euler(-38.278f, 17.012f, 13.473f);
		BaseHh3.transform.localScale = new Vector3 (0.04052505f, 0.1917871f, 0.04052505f);
		//BaseHh3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseHh3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseHh4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseHh4.transform.position = new Vector3(1.0665f, 0.13072f, -0.72051f);
		BaseHh4.transform.rotation *= Quaternion.Euler(-30.11f, -33.526f, -203.751f);
		BaseHh4.transform.localScale = new Vector3 (0.04052505f, 0.1917871f, 0.04052505f);
		//BaseHh4.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseHh4.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseHh5 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseHh5.transform.position = new Vector3(1.142f, 1.294f, -0.719f);
		BaseHh5.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 1.704f);
		BaseHh5.transform.localScale = new Vector3 (0.01740417f, 0.08243375f, 0.01740417f);
		//BaseHh5.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseHh5.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		PlatoHh1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PlatoHh1.transform.position = new Vector3(1.149963f, 1.059013f, -0.7286234f);
		PlatoHh1.transform.rotation *= Quaternion.Euler(1.898f, 3.337f, -7.133f);
		PlatoHh1.transform.localScale = new Vector3 (0.5897473f, 0.006003461f, 0.5897473f);
		//PlatoHh1.GetComponent<Renderer>().material.color = Color.yellow;
		renderPlato = PlatoHh1.GetComponent<Renderer>();
		renderPlato.material.SetTexture("_MainTex", imgPlato);
		
		PlatoHh2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PlatoHh2.transform.position = new Vector3(1.155837f, 1.101487f, -0.7274765f);
		PlatoHh2.transform.rotation *= Quaternion.Euler(1.898f, 3.337f, -7.133f);
		PlatoHh2.transform.localScale = new Vector3 (0.5897473f, 0.006003461f, 0.5897473f);
		//PlatoHh2.GetComponent<Renderer>().material.color = Color.yellow;
		renderPlato = PlatoHh2.GetComponent<Renderer>();
		renderPlato.material.SetTexture("_MainTex", imgPlato);
		
		GameObject BaseR1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseR1.transform.position = new Vector3(0.354f, 0.683f, 0.978f);
		BaseR1.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 1.704f);
		BaseR1.transform.localScale = new Vector3 (0.04012181f, 0.4616832f, 0.04012181f);
		//BaseR1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseR1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseR2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseR2.transform.position = new Vector3(0.3850001f, 0.11f, 0.842f);
		BaseR2.transform.rotation *= Quaternion.Euler(43.802f, -0.033f, 5.622f);
		BaseR2.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseR2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseR2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseR3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseR3.transform.position = new Vector3(0.442f, 0.11f, 1.064f);
		BaseR3.transform.rotation *= Quaternion.Euler(-38.278f, 17.012f, 13.473f);
		BaseR3.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseR3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseR3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject BaseR4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        BaseR4.transform.position = new Vector3(0.259f, 0.113f, 0.9940001f);
		BaseR4.transform.rotation *= Quaternion.Euler(-30.11f, -33.526f, -203.751f);
		BaseR4.transform.localScale = new Vector3 (0.04012181f, 0.1898788f, 0.04012181f);
		//BaseR4.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = BaseR4.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		Ride = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Ride.transform.position = new Vector3(0.3490001f, 1.086f, 0.992f);
		Ride.transform.rotation *= Quaternion.Euler(-9.737f, -4.838f, -24.217f);
		Ride.transform.localScale = new Vector3 (0.7994119f, 0.008137789f, 0.7994119f);
		//Ride.GetComponent<Renderer>().material.color = Color.yellow;
		renderPlato = Ride.GetComponent<Renderer>();
		renderPlato.material.SetTexture("_MainTex", imgPlato);
		
		GameObject PedalRedo1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalRedo1.transform.position = new Vector3(1.296f, 0.026f, -0.754f);
		PedalRedo1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		PedalRedo1.transform.localScale = new Vector3 (0.3487458f, 0.01143302f, 0.1397789f);
		//PedalRedo1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalRedo1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject PedalRedo2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalRedo2.transform.position = new Vector3(1.296f, 0.072f, -0.754f);
		PedalRedo2.transform.rotation *= Quaternion.Euler(0, 0, -25.229f);
		PedalRedo2.transform.localScale = new Vector3 (0.3052213f, 0.01143302f, 0.1397789f);
		//PedalRedo2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalRedo2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject PedalBom1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalBom1.transform.position = new Vector3(0.665f, 0.03f, 0.01100002f);
		PedalBom1.transform.rotation *= Quaternion.Euler(0, 0, 0);
		PedalBom1.transform.localScale = new Vector3 (0.3259304f, 0.01143302f, 0.1397789f);
		//PedalBom1.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalBom1.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject PedalBom2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalBom2.transform.position = new Vector3(0.647f, 0.074f, 0.011f);
		PedalBom2.transform.rotation *= Quaternion.Euler(0, 0, -21.731f);
		PedalBom2.transform.localScale = new Vector3 (0.3052213f, 0.01143302f, 0.1397789f);
		//PedalBom2.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalBom2.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject PedalBom3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalBom3.transform.position = new Vector3(0.5261286f, 0.2028317f, 0.01216705f);
		PedalBom3.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 7.927001f);
		PedalBom3.transform.localScale = new Vector3 (0.04015895f, 0.2228918f, 0.04015895f);
		//PedalBom3.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalBom3.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
		GameObject PedalBom4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        PedalBom4.transform.position = new Vector3(0.4960148f, 0.4148571f, 0.01656844f);
		PedalBom4.transform.rotation *= Quaternion.Euler(1.743f, -3.808f, 7.927001f);
		PedalBom4.transform.localScale = new Vector3 (0.06902073f, 0.04743141f, 0.08006188f);
		//PedalBom4.GetComponent<Renderer>().material.color = Color.grey;
		renderMetal = PedalBom4.GetComponent<Renderer>();
		renderMetal.material.SetTexture("_MainTex", imgMetal);
		
    }
}

