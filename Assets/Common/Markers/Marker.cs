using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class Marker : MonoBehaviour
    {
        public Mark mark;
    }

    public static class MarkerExtensions
    {
        public static bool Marked(this Component component, Mark mark)
        {
            return Marked(component, mark.Single());
        }

        public static bool Marked(this Component component, IEnumerable<Mark> marks) {
            return component.GetComponents<Marker>().Any(marker => marks.Contains(marker.mark));
        }

        public static Marker FindMark(this Component component, Mark mark)
        {
            return component.GetComponentsInChildren<Marker>().Where(marker => marker.mark == mark).FirstOrDefault();
        }
    }
}