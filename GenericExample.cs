namespace LearningDotNet;

/// <summary>
/// Represents a unit of work that returns a result of type <typeparamref name="TResult"/>.
/// This interface uses covariance (via the <c>out</c> keyword), which allows more flexible
/// assignment to variables of base types.
/// </summary>
/// <typeparam name="TResult">The output type of the result produced by the task.</typeparam>

public interface ITask<out TResult>
{
    /// <summary>
    /// Executes the task and returns a result of type <typeparamref name="TResult"/>.
    /// </summary>
    public TResult Perform();
}

/// <summary>
/// A concrete implementation of <see cref="ITask{TResult}"/> that simulates sending an email.
/// </summary>
public class EmailTask(string message, string recipient) : ITask<string>
{
    private string Message { get; } = message;
    private string Recipient { get; } = recipient;
    
    /// <summary>
    /// Simulates sending an email and returns a summary string.
    /// </summary>
    /// <returns>A string confirming the email was "sent".</returns>
    public string Perform()
    {
        return $"Email sent to: {Recipient}, with Message: {Message}";
    }
}

/// <summary>
/// A concrete implementation of <see cref="ITask{TResult}"/> that simulates generating a report.
/// </summary>
public class ReportTask(string reportName) : ITask<string>
{
    private string ReportName { get; } = reportName;
    
    /// <summary>
    /// Simulates generating a report and returns a summary string.
    /// </summary>
    /// <returns>A string indicating the report was generated.</returns>
    public string Perform()
    {
        return $"Report {ReportName} generated by {nameof(EmailTask)}.";
    }
}

/// <summary>
/// A generic processor that can execute any task implementing <see cref="ITask{TResult}"/>.
/// </summary>
/// <typeparam name="TTask">The type of the task to execute. Must implement <see cref="ITask{TResult}"/>.</typeparam>
/// <typeparam name="TResult">The type of result returned by the task.</typeparam>
/// <example>
/// <code>
/// var emailTask = new EmailTask("Welcome!", "john@example.com");
/// var processor = new TaskProcessor&lt;EmailTask, string&gt;(emailTask);
/// var result = processor.Execute(); // result: "Email sent to: john@example.com, with Message: Welcome!"
/// </code>
/// </example>
public class TaskProcessor<TTask, TResult>(TTask task) where TTask : ITask<TResult>
{
    private readonly TTask _task = task;
    
    /// <summary>
    /// Executes the underlying task and returns its result.
    /// </summary>
    /// <returns>The result of executing the task.</returns>
    public TResult Execute()
    {
        return _task.Perform();
    }
}
