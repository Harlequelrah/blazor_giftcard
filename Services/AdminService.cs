using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using blazor_giftcard.Models;
using System.Text.Json;
using System.Text;


namespace blazor_giftcard.Services
{
    public class AdminService
    {
        private readonly HttpClient _authClient;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IHttpClientFactory httpClientFactory, ILogger<AdminService> logger)
        {
            _authClient = httpClientFactory.CreateClient("authClientAPI") ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
        }

        public async Task<Package> GetPackageByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting package with ID {id}.");
                var package = await _authClient.GetFromJsonAsync<Package>($"Package/{id}");
                _logger.LogInformation($"Successfully retrieved package with ID {id}.");
                return package;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving package with ID {id}: {ex.Message}");
                return null;
            }
        }
        public async Task<User> GetUserAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting User with ID {id}.");
                var User = await _authClient.GetFromJsonAsync<User>($"User/{id}");
                _logger.LogInformation($"Successfully retrieved User with ID {id}.");
                return User;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving User with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<Package> CreatePackageAsync(PackageDto packageDto)
        {
            try
            {
                _logger.LogInformation("Creating a new package.");
                var response = await _authClient.PostAsJsonAsync("Package", packageDto);
                response.EnsureSuccessStatusCode();
                var createdPackage = await response.Content.ReadFromJsonAsync<Package>();
                _logger.LogInformation("Successfully created a new package.");
                return createdPackage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating a new package: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdatePackageAsync(int id, Package package)
        {
            try
            {
                _logger.LogInformation($"Updating package with ID {id}.");
                var response = await _authClient.PutAsJsonAsync($"Package/{id}", package);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully updated package with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating package with ID {id}: {ex.Message}");
                return false;
            }
        }
         public async Task<bool> UpdateRoleAsync(int id, Role role)
        {
            try
            {
                _logger.LogInformation($"Updating role with ID {id}.");
                var response = await _authClient.PutAsJsonAsync($"Role/{id}", role);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully updated role with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating role with ID {id}: {ex.Message}");
                return false;
            }
        }

         public async Task<bool> UpdateUserAsync(int id,bool state)
        {
            try
            {
                var user = await GetUserAsync(id);
                user.IsActive = state;
                _logger.LogInformation($"Updating user with ID {id}.");
                var response = await _authClient.PutAsJsonAsync($"User/{id}",new{Id=user.Id,IsActive=state});
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully updated user with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user with ID {id}: {ex.Message}");
                return false;
            }
        }




        public async Task<bool> DeletePackageAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting package with ID {id}.");
                var response = await _authClient.DeleteAsync($"Package/{id}");
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully deleted package with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting package with ID {id}: {ex.Message}");
                return false;
            }
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            try
            {
                _logger.LogInformation("Getting all roles.");
                var response = await _authClient.GetStringAsync("Role");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var valuesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var roles = JsonSerializer.Deserialize<List<Role>>(valuesElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved roles.");
                    return roles;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving roles: {ex.Message}");
                return new List<Role>();
            }
        }
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                _logger.LogInformation("Getting all users.");
                var response = await _authClient.GetStringAsync("User/GetFormatedUsers");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var valuesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var users = JsonSerializer.Deserialize<List<User>>(valuesElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved users.");
                    return users;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving users: {ex.Message}");
                return new List<User>();
            }
        }
        public async Task<List<Subscriber>> GetSubscribersAsync()
        {
            try
            {
                _logger.LogInformation("Getting subscribers.");
                var response = await _authClient.GetStringAsync("Subscriber");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var subscribersElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var subscribers = JsonSerializer.Deserialize<List<Subscriber>>(subscribersElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved subscribers.");
                    return subscribers;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Subscribers: {ex.Message}");
                return new List<Subscriber>(); // Retourne une liste vide en cas d'erreur
            }
        }
        public async Task<List<Beneficiary>> GetBeneficiariesAsync()
        {
            try
            {
                _logger.LogInformation("Getting beneficiaries.");
                var response = await _authClient.GetStringAsync("Beneficiary");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var beneficiariesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var beneficiaries = JsonSerializer.Deserialize<List<Beneficiary>>(beneficiariesElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved beneficiaries.");
                    return beneficiaries;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Beneficiaries: {ex.Message}");
                return new List<Beneficiary>();
            }
        }
        public async Task<List<Merchant>> GetMerchantsAsync()
        {
            try
            {
                _logger.LogInformation("Getting merchants.");
                var response = await _authClient.GetStringAsync("Merchant");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var merchantsElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var merchants = JsonSerializer.Deserialize<List<Merchant>>(merchantsElement.GetRawText(), options);
                    _logger.LogInformation("Successfully retrieved merchants.");
                    return merchants;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Merchants: {ex.Message}");
                return new List<Merchant>();
            }
        }




        // Get a role by ID
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting role with ID {id}.");
                var role = await _authClient.GetFromJsonAsync<Role>($"Role/{id}");
                _logger.LogInformation($"Successfully retrieved role with ID {id}.");
                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving role with ID {id}: {ex.Message}");
                return null;
            }
        }

        // Create a new role
        public async Task<Role> PostRoleAsync(Role role)
        {
            try
            {
                _logger.LogInformation("Creating a new role.");
                var response = await _authClient.PostAsJsonAsync("Role", role);
                response.EnsureSuccessStatusCode();
                var createdRole = await response.Content.ReadFromJsonAsync<Role>();
                _logger.LogInformation("Successfully created a new role.");
                return createdRole;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating a new role: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateWalletAsync(int idWallet , double Montant , string type, int Id)
        {
            try
            {
                _logger.LogInformation($"Updating wallet with ID {idWallet}.");
                var response = await _authClient.PutAsJsonAsync($"Wallet/{type}/{idWallet}", new { Montant = Montant , Id=Id});
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully updated wallet with ID {idWallet}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating wallet with ID {idWallet}: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> PutRoleAsync(int id, RoleDto roleDto)
        {
            try
            {
                _logger.LogInformation($"Updating role with ID {id}.");
                var response = await _authClient.PutAsJsonAsync($"Role/{id}", roleDto);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully updated role with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating role with ID {id}: {ex.Message}");
                return false;
            }
        }

        // Delete a role by ID
        public async Task<bool> DeleteRoleAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting role with ID {id}.");
                var response = await _authClient.DeleteAsync($"Role/{id}");
                response.EnsureSuccessStatusCode();
                _logger.LogInformation($"Successfully deleted role with ID {id}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting role with ID {id}: {ex.Message}");
                return false;
            }
        }
        public async Task<List<Subscription>> GetSubscriptionsByPackageAsync(int packageId)
        {
            try
            {
                _logger.LogInformation($"Getting subscription with ID {packageId}.");
                var response = await _authClient.GetStringAsync($"Subscription/Package/{packageId}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var subscriptionsElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var subscriptions = JsonSerializer.Deserialize<List<Subscription>>(subscriptionsElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved subscription with ID {packageId}.");
                    return subscriptions;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving subscriptions for subscriber ID {packageId}: {ex.Message}");
                return new List<Subscription>();
            }
        }
        public async Task<List<MerchantHistory>> GetMerchantHistoriesAsync(int idMerchant)
        {
            try
            {
                _logger.LogInformation($"Getting history for merchant ID {idMerchant}.");
                var response = await _authClient.GetStringAsync($"Merchant/history/{idMerchant}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var historiesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var histories = JsonSerializer.Deserialize<List<MerchantHistory>>(historiesElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved history for merchant ID {idMerchant}.");
                    return histories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving history for merchant ID {idMerchant}: {ex.Message}");
                return new List<MerchantHistory>();
            }
        }
        public async Task<List<BeneficiaryHistory>> GetBeneficiaryHistoriesAsync(int idBeneficiary)
        {
            try
            {
                _logger.LogInformation($"Getting history for beneficiary ID {idBeneficiary}.");
                var response = await _authClient.GetStringAsync($"Beneficiary/history/{idBeneficiary}");
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;
                    var historiesElement = root.GetProperty("$values");
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var histories = JsonSerializer.Deserialize<List<BeneficiaryHistory>>(historiesElement.GetRawText(), options);
                    _logger.LogInformation($"Successfully retrieved history for beneficiary ID {idBeneficiary}.");
                    return histories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving history for beneficiary ID {idBeneficiary}: {ex.Message}");
                return new List<BeneficiaryHistory>();
            }
        }
         public async Task<Admin> RegisterAdminAsync(Admin admin)
        {
            try
            {
                _logger.LogInformation($"Registering Admin");
                var response = await _authClient.PostAsJsonAsync($"User/register/admin",admin);
                response.EnsureSuccessStatusCode();
                var registeredAdmin= await response.Content.ReadFromJsonAsync<Admin>();
                _logger.LogInformation($"Successfully registered admin");
                return registeredAdmin;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering admin {ex.Message}");
                return null;
            }
        }
        public async Task<Role> RegisterRoleAsync(RoleDto role)
        {
            try
            {
                _logger.LogInformation($"Registering Role");
                var response = await _authClient.PostAsJsonAsync($"Role",role);
                response.EnsureSuccessStatusCode();
                var registeredRole= await response.Content.ReadFromJsonAsync<Role>();
                _logger.LogInformation($"Successfully registered role");
                return registeredRole;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering role {ex.Message}");
                return null;
            }
        }
        public async Task<Package> RegisterPackageAsync(PackageDto package)
        {
            try
            {
                _logger.LogInformation($"Registering Package");
                var response = await _authClient.PostAsJsonAsync("Package",package);
                response.EnsureSuccessStatusCode();
                var registeredPackage= await response.Content.ReadFromJsonAsync<Package>();
                _logger.LogInformation($"Successfully registered role");
                return registeredPackage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering package {ex.Message}");
                return null;
            }
        }



    }
}
