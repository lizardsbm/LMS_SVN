/// <summary>
///  CSV 로드 데이터의 원형.
/// </summary>
public abstract class BaseCsv {

    //셀 구분자.
    public const char DELIMITER = '$';
    public const char DELIMITER_SUB = '|';
    public const char DELIMITER_AND = '&';


    //리드라인이 정상적으로 완료되었는지.
    public bool isParsed {
        get; private set;
    }

    public void parseLine(string line) {

        if(string.IsNullOrEmpty(line)) {
            isParsed = false;
            return;
        }


        isParsed = parse(line.Split(DELIMITER));
    }


    /// <summary>
    /// 실제 상속받은 child 클래스에서 parse 작업을 수행하는 메서드.
    /// </summary>
    /// <param name="tokens">csv 데이터</param>
    /// <returns></returns>
    protected abstract bool parse(string[] tokens);

}