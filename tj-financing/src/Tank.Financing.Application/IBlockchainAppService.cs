using AElf.EventHandler;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Tank.Financing;

public interface IBlockchainAppService
{
    
}

public class BlockchainAppService : IBlockchainAppService, ITransientDependency
{
    private readonly NodeManager _nodeManager;

    public BlockchainOptions Options { get; }

    public BlockchainAppService(IOptionsSnapshot<BlockchainOptions> options)
    {
        Options = options.Value;
        _nodeManager = new(Options.Endpoint,
            Options.OwnerAddress,
            Options.OwnerPassword);
    }
}