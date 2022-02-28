//Originally from AssetFactory
namespace BulletBox.IO
{
	public interface IByteConvertible
	{
		void FromBytes(byte[] bytes);
		byte[] ToBytes();
	}
}