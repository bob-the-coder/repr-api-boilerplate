namespace ServiceContracts.Contract;

public record OperationResult<TResult>(TResult? Result, bool Success, string? ErrorMessage)
{
    /// <summary>
    /// Gets the result of the operation.
    /// </summary>
    /// <returns>The result of the operation</returns>
    /// <exception cref="NullReferenceException"></exception>
    public TResult GetResult() => Result ?? throw new NullReferenceException();
    
    /// <summary>
    /// Returns the result of a successful operation, using the provided result.
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static OperationResult<TResult> SuccessResult(TResult result)
    {
        return new OperationResult<TResult>(result, true, string.Empty);
    }

    /// <summary>
    /// Returns the result of a failed operation, using the provided error message.
    /// </summary>
    /// <param name="errorMessage">The error message to be used</param>
    /// <returns></returns>
    public static OperationResult<TResult> ErrorResult(string? errorMessage = "")
    {
        return new OperationResult<TResult>(default, false, errorMessage);
    }

    /// <summary>
    /// Returns a new result based on the result's value, without an error message.
    /// </summary>
    /// <param name="result">The result of the operation</param>
    /// <returns></returns>
    public static OperationResult<TResult> FromResult(TResult? result)
    {
        return FromResult(result, string.Empty);
    }

    /// <summary>
    /// Returns a new result based on the result's value, using the provided error message if the value is null.
    /// </summary>
    /// <param name="result">The result of the operation</param>
    /// <param name="errorMessage">The error message to be used if the result is null</param>
    /// <returns></returns>
    public static OperationResult<TResult> FromResult(TResult? result, string errorMessage)
    {
        return new OperationResult<TResult>(result, result is not null, errorMessage);
    }

    /// <summary>
    /// Returns a successful result if all results are successful, using the provided error message if the condition fails.
    /// </summary>
    /// <param name="results">The list of results to aggregate</param>
    /// <param name="errorMessage">The error message to be used if condition fails</param>
    /// <returns></returns>
    public static OperationResult<TResult> All(IEnumerable<OperationResult<TResult?>> results, string errorMessage = "")
    {
        return new OperationResult<TResult>(default, results.All(result => result.Success), errorMessage);
    }
    
    /// <summary>
    /// Returns a successful result if any of the results is successful, using the provided error message if the condition fails.
    /// </summary>
    /// <param name="results">The list of results to aggregate</param>
    /// <param name="errorMessage">The error message to be used if condition fails</param>
    /// <returns></returns>
    public static OperationResult<TResult> Any(IEnumerable<OperationResult<TResult?>> results, string errorMessage = "")
    {
        return new OperationResult<TResult>(default, results.Any(result => result.Success), errorMessage);
    }
}