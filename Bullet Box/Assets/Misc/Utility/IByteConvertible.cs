//Originally from AssetFactory
namespace AssetFactory.IO
{
	public interface IByteConvertible
	{
		void FromBytes(byte[] bytes);
		byte[] ToBytes();
	}
}