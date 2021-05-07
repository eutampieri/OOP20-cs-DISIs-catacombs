using Tampieri.Model;

namespace Tampieri.Ui
{
    /// <summary>This interface should be implemented for every entity that needs to be rendered on screen</summary>
    public interface IAnimatable
    {
        /// <summery>Wether this entity can perform an action or not</summary>
        bool CanPerform(Action a);
    }
}
