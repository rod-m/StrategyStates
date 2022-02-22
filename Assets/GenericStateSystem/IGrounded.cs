using UnityEngine;

namespace GenericStateSystem
{
    public interface IGrounded
    {
        float collissionOverLapRadius { get; set; }
        LayerMask whatIsGround { get; set; }
    }
}