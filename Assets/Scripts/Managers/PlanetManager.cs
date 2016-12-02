using UnityEngine;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{

    #region Variables

    #region Instance

    private static PlanetManager _instance;

    public static PlanetManager instance
    { get { return _instance; } }

    #endregion

    #region Components

    public List<PlanetObject> planets;

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    void Awake()
    {
        if (!_instance)
            _instance = this;

        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        planets = new List<PlanetObject>();

        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            planets.Add(planet.GetComponent<PlanetObject>());
	}

    #endregion

    #region Public

    public PlanetObject ClosestPlanet(Vector3 objectPosition)
    {
        float dist = float.MaxValue;
        PlanetObject closest = null;

        foreach(PlanetObject planet in planets)
        {
            float distTest = Vector3.Distance(planet.transform.position, objectPosition) - Vector3.Magnitude(planet.transform.localScale) * 0.5f;

            if (distTest < dist)
            {
                dist = distTest;
                closest = planet;
            }
        }

        return closest;
    }

    #endregion

    #region Management

    void InitializePlanetList()
    {
        planets = new List<PlanetObject>();

        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
            planets.Add(planet.GetComponent<PlanetObject>());
    }

    void AddToPlanetList(PlanetObject planet)
    { planets.Add(planet); }

    void RemoveFromPlanetList(PlanetObject planet)
    { planets.Remove(planet); }

    #endregion

    #endregion

}
