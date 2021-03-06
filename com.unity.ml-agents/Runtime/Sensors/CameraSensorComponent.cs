using UnityEngine;
using UnityEngine.Serialization;

namespace MLAgents.Sensors
{
    /// <summary>
    /// A SensorComponent that creates a <see cref="CameraSensor"/>.
    /// </summary>
    [AddComponentMenu("ML Agents/Camera Sensor", (int)MenuGroup.Sensors)]
    public class CameraSensorComponent : SensorComponent
    {
        [HideInInspector, SerializeField, FormerlySerializedAs("camera")]
        Camera m_Camera;

        CameraSensor m_Sensor;

        /// <summary>
        /// Camera object that provides the data to the sensor.
        /// </summary>
        public new Camera camera
        {
            get { return m_Camera;  }
            set { m_Camera = value; UpdateSensor(); }
        }

        [HideInInspector, SerializeField, FormerlySerializedAs("sensorName")]
        string m_SensorName = "CameraSensor";

        /// <summary>
        /// Name of the generated <see cref="CameraSensor"/> object.
        /// </summary>
        public string sensorName
        {
            get { return m_SensorName;  }
            internal set { m_SensorName = value;  }
        }

        [HideInInspector, SerializeField, FormerlySerializedAs("width")]
        int m_Width = 84;

        /// <summary>
        /// Width of the generated observation.
        /// </summary>
        public int width
        {
            get { return m_Width;  }
            internal set { m_Width = value;  }
        }

        [HideInInspector, SerializeField, FormerlySerializedAs("height")]
        int m_Height = 84;

        /// <summary>
        /// Height of the generated observation.
        /// </summary>
        public int height
        {
            get { return m_Height;  }
            internal set { m_Height = value;  }
        }

        [HideInInspector, SerializeField, FormerlySerializedAs("grayscale")]
        public bool m_Grayscale;

        /// <summary>
        /// Whether to generate grayscale images or color.
        /// </summary>
        public bool grayscale
        {
            get { return m_Grayscale;  }
            internal set { m_Grayscale = value;  }
        }

        [HideInInspector, SerializeField, FormerlySerializedAs("compression")]
        SensorCompressionType m_Compression = SensorCompressionType.PNG;

        /// <summary>
        /// The compression type to use for the sensor.
        /// </summary>
        public SensorCompressionType compression
        {
            get { return m_Compression;  }
            set { m_Compression = value; UpdateSensor(); }
        }

        /// <summary>
        /// Creates the <see cref="CameraSensor"/>
        /// </summary>
        /// <returns>The created <see cref="CameraSensor"/> object for this component.</returns>
        public override ISensor CreateSensor()
        {
            m_Sensor = new CameraSensor(m_Camera, m_Width, m_Height, grayscale, m_SensorName, compression);
            return m_Sensor;
        }

        /// <summary>
        /// Computes the observation shape of the sensor.
        /// </summary>
        /// <returns>The observation shape of the associated <see cref="CameraSensor"/> object.</returns>
        public override int[] GetObservationShape()
        {
            return CameraSensor.GenerateShape(m_Width, m_Height, grayscale);
        }

        /// <summary>
        /// Update fields that are safe to change on the Sensor at runtime.
        /// </summary>
        internal void UpdateSensor()
        {
            if (m_Sensor != null)
            {
                m_Sensor.camera = m_Camera;
                m_Sensor.compressionType = m_Compression;
            }
        }
    }
}
