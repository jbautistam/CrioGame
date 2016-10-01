using System;

using Bau.Libraries.CrioGame.Common.Models.Graphics;
using Bau.Libraries.CrioGame.Common.Models.Structs;

namespace Bau.Libraries.CrioGame.Common.Interfaces.GraphicsEngine
{
	/// <summary>
	///		Interface para los lotes de dibujo de sprites
	/// </summary>
	public interface ISpriteBatch
	{
		/// <summary>
		///		Comienza un lote de dibujo
		/// </summary>
		void Begin();

		/// <summary>
		///		Obtiene las dimensiones de una imagen
		/// </summary>
		Size2D GetDimensions(AbstractImageModelBase objImage);

		/// <summary>
		///		Dibuja una imagen
		/// </summary>
		void Draw(AbstractImageModelBase objImage);

		/// <summary>
		///		Dibuja una imagen utilizando una vista
		/// </summary>
		void Draw(AbstractImageModelBase objImage, Rectangle rctView);

		/// <summary>
		///		Dibuja una imagen escalada a partir de parte de una imagen
		/// </summary>
		void DrawFull(AbstractImageModelBase objImage, Rectangle rctCamera);

		/// <summary>
		///		Dibuja un texto
		/// </summary>
		void DrawText(AbstractTextModel objText);

		/// <summary>
		///		Dibuja un texto utilizando una vista
		/// </summary>
		void DrawText(AbstractTextModel objText, Rectangle rctView);

		/// <summary>
		///		Finaliza un lote de dibujo
		/// </summary>
		void End();
	}
}
