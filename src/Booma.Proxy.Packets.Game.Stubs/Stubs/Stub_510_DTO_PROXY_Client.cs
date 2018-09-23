using FreecraftCore;
using FreecraftCore.Serializer;
using Booma.Proxy;

[WireDataContractBaseLinkAttribute(510, typeof(PSOBBGamePacketPayloadClient))]
[WireDataContractAttribute]
public sealed class Stub_0x1FE_DTO_PROXY_Client : PSOBBGamePacketPayloadClient, IUnknownPayloadType
{
    [ReadToEndAttribute]
    [WireMemberAttribute(1)]
    private byte[] _UnknownBytes;
    public byte[] UnknownBytes
    {
        get
        {
            return _UnknownBytes;
        }

        set
        {
            _UnknownBytes = value;
        }
    }

    public Stub_0x1FE_DTO_PROXY_Client()
    {
    }
}