using System;

namespace Tracer.Interfaces
{
    public interface ISerializer
    {
        string toJSON();
        string toXML();
    }
}
