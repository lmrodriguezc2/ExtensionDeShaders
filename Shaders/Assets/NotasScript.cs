using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotasScript : MonoBehaviour
{
	public int cantidadPorNota;

	float deltax = 0.01f;
	float deltay;
	float sen1;
	float sen2 = 0;
	float transformation = 1;
	float actualx = 0;
	Vector3 mov;

	// Start is called before the first frame update
	void Start()
	{
		GameObject objetoBase = GameObject.Find("Base");
		for(int i = 0; i < cantidadPorNota; i++)
        {
			GameObject nt = MakeNota1(i);
			nt.transform.Translate(notColliding());
			nt.transform.Rotate(0.0f, Random.Range(0.0f, 359.0f), 0.0f, Space.Self);
			nt.transform.parent = objetoBase.transform;
		}
		for (int i = 0; i < cantidadPorNota; i++)
		{
			GameObject nt = MakeNota2(i);
			nt.transform.Translate(notColliding());
			nt.transform.Rotate(0.0f, Random.Range(0.0f, 359.0f), 0.0f, Space.Self);
			nt.transform.parent = objetoBase.transform;
		}
	}

    private void Update()
	{
		GameObject objetoBase = GameObject.Find("Base");
		foreach (Transform child in objetoBase.transform)
		{
			sen1 = sen2;
			sen2 = Mathf.Sin(Mathf.Deg2Rad * actualx * transformation);
			mov = new Vector3(deltax, (sen2 - sen1) * 9.0f, 0);
			child.Translate(mov);
			child.position = outOfBounds(child.position);
			actualx += deltax * 20;
			if (actualx == 360)
				actualx = 0;
		}
	}

    GameObject MakeNota1(int numero)
	{
		GameObject Nota1 = new GameObject("NotaG1-" + numero);
		GameObject Nota10 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota10.transform.SetParent(Nota1.transform);
		Nota10.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		Nota10.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota10.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota11 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota11.transform.SetParent(Nota1.transform);
		Nota11.transform.position = new Vector3(0.0f, 0.0f, 0.076f);
		Nota11.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota11.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota12 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota12.transform.SetParent(Nota1.transform);
		Nota12.transform.position = new Vector3(0.0f, 0.046f, 0.0384f);
		Nota12.transform.rotation *= Quaternion.Euler(90, -1.525879e-05f, -1.525879e-05f);
		Nota12.transform.localScale = new Vector3(0.01288273f, 0.08640555f, 0.01065468f);
		Nota12.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota13 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota13.transform.SetParent(Nota1.transform);
		Nota13.transform.position = new Vector3(0.0026688f, -0.0587f, -0.0091f);
		Nota13.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota13.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota13.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota14 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota14.transform.SetParent(Nota1.transform);
		Nota14.transform.position = new Vector3(0.0037688f, -0.058f, 0.064f);
		Nota14.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota14.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota14.GetComponent<Renderer>().material.color = Color.red;

		return Nota1;
	}

	GameObject MakeNota2(int numero)
	{
		GameObject Nota2 = new GameObject("NotaG2-" + numero);

		GameObject Nota20 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota20.transform.SetParent(Nota2.transform);
		Nota20.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		Nota20.transform.localScale = new Vector3(0.01288273f, 0.09791894f, 0.01065468f);
		Nota20.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota21 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Nota21.transform.SetParent(Nota2.transform);
		Nota21.transform.position = new Vector3(-0.00000001f, 0.0402f, 0.0171f);
		Nota21.transform.rotation *= Quaternion.Euler(101.614f, -1.525879e-05f, -1.525879e-05f);
		Nota21.transform.localScale = new Vector3(0.01288273f, 0.04592196f, 0.01065468f);
		Nota21.GetComponent<Renderer>().material.color = Color.red;

		GameObject Nota22 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Nota22.transform.SetParent(Nota2.transform);
		Nota22.transform.position = new Vector3(0.0026688f, -0.0587f, -0.0091f);
		Nota22.transform.rotation *= Quaternion.Euler(31.436f, 15.413f, 8.682f);
		Nota22.transform.localScale = new Vector3(0.03016089f, 0.04271687f, 0.03016089f);
		Nota22.GetComponent<Renderer>().material.color = Color.red;

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
}
