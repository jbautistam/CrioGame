using System;

namespace Bau.Libraries.CrioGame.Common.Models.Structs
{
	/// <summary>
	///		Estructura con los datos de un vector 2D
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DebugString")]
	public struct Vector2D : IEquatable<Vector2D>
	{	
		public Vector2D(float fltX = 0, float fltY = 0) 
		{ X = fltX;
			Y = fltY;
		}

		/// <summary>
		///		Añade un punto a este vector
		/// </summary>
		public Vector2D Add(int intX, int intY)
		{ return new Vector2D(X + intX, Y + intY);
		}

		/// <summary>
		///		Añade este vector al destino
		/// </summary>
		public Vector2D Add(Vector2D vctTarget)
		{ return new Vector2D(X + vctTarget.X, Y + vctTarget.Y);
		}

		/// <summary>
		///		Resta este vector del destino
		/// </summary>
		public Vector2D Substract(Vector2D vctTarget)
		{ return new Vector2D(X - vctTarget.X, Y - vctTarget.Y);
		}

		/// <summary>
		///		Obtiene un vector normalizado
		/// </summary>
		public Vector2D Normalize()
		{	double dblLength = Length;

				if (dblLength == 0)
					return new Vector2D(0, 0);
				else
					return new Vector2D((float) (X / dblLength), (float) (Y / dblLength));
		}

		/// <summary>
		///		Producto escalar
		/// </summary>
		public float DotProduct(Vector2D	vctTarget)
		{	return X * vctTarget.X + Y + vctTarget.Y;
		}

		/// <summary>
		///		Calcula la distancia entre dos vectores
		/// </summary>
		public float ComputeDistance(Vector2D vctTarget)
		{	return (float) Math.Sqrt(Math.Pow(X - vctTarget.X, 2.0) + Math.Pow(Y - vctTarget.Y, 2.0));
		}

		/// <summary>
		///		Calcula la distancia entre dos vectores
		/// </summary>
		public float ComputeSquaredDistance(Vector2D vctTarget)
		{	return (float) (Math.Pow(vctTarget.X - X, 2) + Math.Pow(vctTarget.Y - Y, 2));
		}

		/// <summary>
		///		Calcula el ángulo entre dos vectores
		/// </summary>
		public float ComputeAngle(Vector2D vctTarget)
		{ Vector2D vctFacing = new Vector2D(0,1).Normalize();
			Vector2D vctSubstract = Substract(vctTarget);

				// Normaliza el vector de la distancia
					vctSubstract.Normalize();
				// Devuelve el ángulo entre los dos
					return (float) Math.Acos(vctSubstract.DotProduct(vctFacing));
		}

		/// <summary>
		///		Transforma el vector a coordenadas polares
		/// </summary>
		public Polar2D ToPolar()
		{ return new Polar2D(Heading, Length);
		}
 
    /// <summary>
    ///		Copara si el vector es igual a <see cref="Object"/>
    /// </summary>
    public override bool Equals(object objVector)
    {	if (objVector is Vector2D)
        return Equals((Vector2D) objVector);
			else
        return false;
    }

    /// <summary>
    ///		Copara si el vector es igual a <see cref="Vector2D"/>
    /// </summary>
    public bool Equals(Vector2D vctVector)
    {	return X == vctVector.X && Y == vctVector.Y;
    }

    /// <summary>
    ///		Obtiene el código hash de <see cref="Vector2D"/>
    /// </summary>
    public override int GetHashCode()
    { return X.GetHashCode() + Y.GetHashCode();
    }

    /// <summary>
    ///		Invierte los valores de <see cref="Vector2D"/>
    /// </summary>
    public static Vector2D operator -(Vector2D vctVector)
    {	// Invierte las coordenadas
				vctVector.X = -vctVector.X;
				vctVector.Y = -vctVector.Y;
			// Devuelve el vector convertido
        return vctVector;
    }

    /// <summary>
    ///		Suma dos vectores
    /// </summary>
    public static Vector2D operator +(Vector2D vctVector1, Vector2D vctVector2)
    {	// Suma los componentes
        vctVector1.X += vctVector2.X;
        vctVector1.Y += vctVector2.Y;
			// Devuelve el vector
        return vctVector1;
    }

    /// <summary>
    ///		Resta un <see cref="Vector2D"/> de otro <see cref="Vector2D"/>
    /// </summary>
    public static Vector2D operator -(Vector2D vctVector1, Vector2D vctVector2)
    { // Resta los componentes
        vctVector1.X -= vctVector2.X;
        vctVector1.Y -= vctVector2.Y;
			// Devuelve el vector
        return vctVector1;
    }

    /// <summary>
    ///		Multiplica los componentes de dos vectores entre sí
    /// </summary>
    public static Vector2D operator *(Vector2D vctVector1, Vector2D vctVector2)
    {	// Multiplica los componentes
				vctVector1.X *= vctVector2.X;
				vctVector1.Y *= vctVector2.Y;
			// Devuelve el nuevo vector
				return vctVector1;
    }

    /// <summary>
    ///		Multiplica los componentes de un vector por un escalar
    /// </summary>
    public static Vector2D operator *(Vector2D vctVector, float fltScale)
    {	// Multiplica los componentes por el escalar
				vctVector.X *= fltScale;
				vctVector.Y *= fltScale;
			// Devuelve el vector
				return vctVector;
    }

    /// <summary>
    ///		Multiplica los componentes de un vector por un escalar
    /// </summary>
    public static Vector2D operator *(float fltScale, Vector2D vctVector)
    {	// Multiplica los componentes por el escalar
				vctVector.X *= fltScale;
				vctVector.Y *= fltScale;
			// Devuelve el vector
				return vctVector;
    }

    /// <summary>
    ///		Divide los componentes de un <see cref="Vector2D"/> por los componentes de otro <see cref="Vector2D"/>.
    /// </summary>
    public static Vector2D operator /(Vector2D vctVector1, Vector2D vctVector2)
    {	// Divide los componentes
				vctVector1.X /= vctVector2.X;
				vctVector1.Y /= vctVector2.Y;
			// Devuelve el vector
				return vctVector1;
    }

    /// <summary>
		///		Divide los componentes de <see cref="Vector2D"/> por un escalar
    /// </summary>
    public static Vector2D operator /(Vector2D vctVector, float fltDivider)
    { // Divide los componentes por el escalar
				vctVector.X /= fltDivider;
				vctVector.Y /= fltDivider;
			// Devuelve el vector
				return vctVector;
    }

    /// <summary>
    ///		Comprueba si dos <see cref="Vector2D"/> son iguales
    /// </summary>
    public static bool operator ==(Vector2D vctVector1, Vector2D vctVector2)
    {	return vctVector1.X == vctVector2.X && vctVector1.Y == vctVector2.Y;
    }

    /// <summary>
    ///		Comprueba si dos <see cref="Vector2D"/> son distintos
    /// </summary>
    public static bool operator !=(Vector2D vctVector1, Vector2D vctVector2)
    { return vctVector1.X != vctVector2.X || vctVector1.Y != vctVector2.Y;
    }

		/// <summary>
		///		Coordenada X 
		/// </summary>
		public float X { get; set; }

		/// <summary>
		///		Coordenada X 
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		///		Longitud del vector
		/// </summary>
		public float Length 
		{ get { return (float) Math.Sqrt(X * X + Y * Y); }
		}

		/// <summary>
		///		Longitud del vector al cuadrado (se utiliza por rendimiento)
		/// </summary>
		public float LengthSquared
		{ get { return X * X + Y * Y; }
		}

		/// <summary>
		///		Obtiene el ángulo hacia donde apunta el vector
		/// </summary>
		public float Heading
		{ get 
				{ if (X == 0)
						return 0;
					else
						return (float) Math.Atan(Y / X); 
				}
		}

		/// <summary>
		///		Cadena de depuración
		/// </summary>
		public string DebugString
		{ get { return $"({X}, {Y})"; }
		}
	}
}
