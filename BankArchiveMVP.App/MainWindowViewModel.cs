using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BankArchiveMVP.Application.UseCases.Cases;
using BankArchiveMVP.Application.UseCases.Customers;
using BankArchiveMVP.Application.UseCases.Documents;
using BankArchiveMVP.Domain.Enums;

namespace BankArchiveMVP.App;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly CreateCustomerService _createCustomer;
    private readonly CreateCaseService _createCase;
    private readonly CreateDocumentService _createDocument;

    public MainWindowViewModel(
        CreateCustomerService createCustomer,
        CreateCaseService createCase,
        CreateDocumentService createDocument)
    {
        _createCustomer = createCustomer;
        _createCase = createCase;
        _createDocument = createDocument;

        SelectedCaseType = CaseType.Loan;

        CreateCustomerCommand = new AsyncRelayCommand(CreateCustomerAsync);
        CreateCaseCommand = new AsyncRelayCommand(CreateCaseAsync);
        CreateDocumentCommand = new AsyncRelayCommand(CreateDocumentAsync);
    }

    public string Title => "Bank Archive MVP";

    // ================= CUSTOMER =================
    public string CustomerNo { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? NationalId { get; set; }

    private string _customerStatusText = "";
    public string CustomerStatusText
    {
        get => _customerStatusText;
        private set { _customerStatusText = value; OnPropertyChanged(); }
    }

    public ICommand CreateCustomerCommand { get; }

    private async Task CreateCustomerAsync()
    {
        try
        {
            var id = await _createCustomer.CreateAsync(CustomerNo, FirstName, LastName, NationalId);
            CustomerStatusText = $"Customer created. Id: {id}";
        }
        catch (Exception ex)
        {
            CustomerStatusText = ex.Message;
        }
    }

    // ================= CASE =================
    public string CaseNo { get; set; } = "";
    public string CaseCustomerNo { get; set; } = "";

    private CaseType _selectedCaseType;
    public CaseType SelectedCaseType
    {
        get => _selectedCaseType;
        set { _selectedCaseType = value; OnPropertyChanged(); }
    }

    public IEnumerable<CaseType> CaseTypes =>
        Enum.GetValues(typeof(CaseType)).Cast<CaseType>();

    private string _caseStatusText = "";
    public string CaseStatusText
    {
        get => _caseStatusText;
        private set { _caseStatusText = value; OnPropertyChanged(); }
    }

    public ICommand CreateCaseCommand { get; }

    private async Task CreateCaseAsync()
    {
        try
        {
            var id = await _createCase.CreateAsync(CaseNo, CaseCustomerNo, SelectedCaseType);
            CaseStatusText = $"Case created. Id: {id}";
        }
        catch (Exception ex)
        {
            CaseStatusText = ex.Message;
        }
    }

    // ================= DOCUMENT =================
    public string DocumentCaseNo { get; set; } = "";
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";
    public string FileHash { get; set; } = "";
    public string DocumentType { get; set; } = "";
    public string? DocumentTitle { get; set; }

    private string _documentStatusText = "";
    public string DocumentStatusText
    {
        get => _documentStatusText;
        private set { _documentStatusText = value; OnPropertyChanged(); }
    }

    public ICommand CreateDocumentCommand { get; }

    private async Task CreateDocumentAsync()
    {
        try
        {
            var id = await _createDocument.CreateAsync(
                DocumentCaseNo,
                FileName,
                FilePath,
                FileHash,
                DocumentType,
                DocumentTitle);

            DocumentStatusText = $"Document created. Id: {id}";
        }
        catch (Exception ex)
        {
            DocumentStatusText = ex.Message;
        }
    }

    // ================= INotify =================
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
