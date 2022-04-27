using GM.Enums;

namespace GM.VM.Variables;

public class NumberVariable : Variable
{
	public override VariableType Type => VariableType.Number;

	public float Value { get; set; }

	public NumberVariable( float value )
	{
		Value = value;
	}

	public override Variable Clone( Context _ ) => new NumberVariable( Value );

	public override Variable Get( Context _ ) => this;
}
