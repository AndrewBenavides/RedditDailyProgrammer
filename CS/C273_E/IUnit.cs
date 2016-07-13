namespace C273_E {
    public interface IUnit {
        char Code { get; }
        decimal Value { get; set; }

        IUnit ConvertTo(char code);
    }
}
