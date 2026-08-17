using EmployeeAPI.Services.TransformationService;
using Fluid;

public class TransformationService : ITransformationService
{
    public async Task<string> TransformEmployeeDataBasedOnTemplateAsync(
        string template,
        object model)
    {
        var parser = new FluidParser();

        if (!parser.TryParse(template, out var fluidTemplate))
        {
            throw new Exception("Invalid Liquid template");
        }

        var context = new TemplateContext(model);

        return await fluidTemplate.RenderAsync(
            context);
    }
}