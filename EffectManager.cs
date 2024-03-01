using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [System.Serializable]
    private class Entry
    {
        public string tag;
        public int priority;
        public GameObject effect;
    }

    [SerializeField]
    private List<Entry> destructionEffects = new();

    void OnEnable()
    {
        ObjectRegistry.OnBlockDestroyed += this.OnBlockDestroyed;
    }

    private IEnumerable<GameObject> GetEffectsForTag(string tag)
    {
        return this.destructionEffects.Where(effect => effect.tag == tag).OrderBy(effect => effect.priority);
    }

    private void OnBlockDestroyed(GameObject gameObject)
    {
        var effects = this.GetEffectsForTag(gameObject.tag);

        Debug.Log($"{gameObject.name} destroyed, found {effects.Count()} effects");

        foreach (var entry in effects)
        {
            if (gameObject.tag == entry.tag)
                Object.Instantiate(entry.effect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
