using FreecraftCore;
using FreecraftCore.Serializer;
using Booma.Proxy;

[WireDataContractBaseLinkAttribute(15, typeof(PSOBBGamePacketPayloadServer))]
[WireDataContractAttribute]
public sealed class Stub_0xF_DTO_PROXY_Server : PSOBBGamePacketPayloadServer, IUnknownPayloadType
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

    public Stub_0xF_DTO_PROXY_Server()
    {
    }
}