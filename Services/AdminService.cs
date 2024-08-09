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

        // Get a single package by ID
        public async Task<Package> GetPackageAsync(int id)
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
         public async Task<bool> UpdateUserAsync(int id,bool state)
        {
            try
            {
                var user = await GetUserAsync(id);
                Console.WriteLine("id:"+user.Id);
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


        // Get a role by ID
        public async Task<Role> GetRoleAsync(int id)
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

        // Update an existing role
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
    }
}
