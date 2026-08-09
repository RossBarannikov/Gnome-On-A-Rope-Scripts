using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	public GameObject ropeSegmentPrefab;

	private List<GameObject> ropeSegments = new List<GameObject>();

	public Rigidbody2D connectedObject;

	public float maxRopeSegmentLength = 1f;

	public float ropeSpeed = 4f;

	private LineRenderer lineRenderer;

	public bool isIncreasing { get; set; }

	public bool isDecreasing { get; set; }

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		ResetLength();
	}

	public void ResetLength()
	{
		foreach (GameObject ropeSegment in ropeSegments)
		{
			Object.Destroy(ropeSegment);
		}
		ropeSegments = new List<GameObject>();
		isDecreasing = false;
		isIncreasing = false;
		CreateRopeSegment();
	}

	private void CreateRopeSegment()
	{
		GameObject gameObject = Object.Instantiate(ropeSegmentPrefab, base.transform.position, Quaternion.identity);
		gameObject.transform.SetParent(base.transform, worldPositionStays: true);
		Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
		SpringJoint2D component2 = gameObject.GetComponent<SpringJoint2D>();
		if (component == null || component2 == null)
		{
			Debug.LogError("Rope segment body prefab has no Rigidbody2D and/or SpringJoint2D!");
			return;
		}
		ropeSegments.Insert(0, gameObject);
		if (ropeSegments.Count == 1)
		{
			SpringJoint2D component3 = connectedObject.GetComponent<SpringJoint2D>();
			component3.connectedBody = component;
			component3.distance = 0.1f;
			component2.distance = maxRopeSegmentLength;
		}
		else
		{
			ropeSegments[1].GetComponent<SpringJoint2D>().connectedBody = component;
			component2.distance = 0f;
		}
		component2.connectedBody = GetComponent<Rigidbody2D>();
	}

	private void RemoveRopeSegment()
	{
		if (ropeSegments.Count >= 2)
		{
			GameObject obj = ropeSegments[0];
			ropeSegments[1].GetComponent<SpringJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
			ropeSegments.RemoveAt(0);
			Object.Destroy(obj);
		}
	}

	private void Update()
	{
		SpringJoint2D component = ropeSegments[0].GetComponent<SpringJoint2D>();
		if (isIncreasing)
		{
			if (component.distance >= maxRopeSegmentLength)
			{
				CreateRopeSegment();
			}
			else
			{
				component.distance += ropeSpeed * Time.deltaTime;
			}
		}
		if (isDecreasing)
		{
			if (component.distance <= 0.005f)
			{
				RemoveRopeSegment();
			}
			else
			{
				component.distance -= ropeSpeed * Time.deltaTime;
			}
		}
		if (lineRenderer != null)
		{
			lineRenderer.positionCount = ropeSegments.Count + 2;
			lineRenderer.SetPosition(0, base.transform.position);
			for (int i = 0; i < ropeSegments.Count; i++)
			{
				lineRenderer.SetPosition(i + 1, ropeSegments[i].transform.position);
			}
			SpringJoint2D component2 = connectedObject.GetComponent<SpringJoint2D>();
			lineRenderer.SetPosition(ropeSegments.Count + 1, connectedObject.transform.TransformPoint(component2.anchor));
		}
	}
}
