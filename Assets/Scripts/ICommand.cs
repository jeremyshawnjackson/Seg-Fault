using UnityEngine;

namespace Redux
{
    public interface ICommand
    {
        void Execute(GameObject gameObject);
    }
}