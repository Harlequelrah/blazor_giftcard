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

                // Récupération de la réponse brute sous forme de chaîne de caractères
                var response = await _authClient.GetStringAsync("Role");

                // Impression de la réponse brute pour examen

                // Analyse de la réponse JSON
                using (var document = JsonDocument.Parse(response))
                {
                    var root = document.RootElement;

                    // Impression du document JSON complet

                    // Extraction de l'élément $values
                    var valuesElement = root.GetProperty("$values");

                    // Impression de l'élément $values
                    Console.WriteLine("Values Element: " + valuesElement);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Désérialisation des données
                    var roles = JsonSerializer.Deserialize<List<Role>>(valuesElement.GetRawText(), options);

                    _logger.LogInformation("Successfully retrieved roles.");

                    foreach (var role in roles)
                    {
                        Console.WriteLine("Role: " + role.Id + ", RoleNom: " + role.RoleNom);
                    }

                    return roles;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving roles: {ex.Message}");
                return new List<Role>();
            }
        }

        // Get a role by ID
        public async Task<Role> GetRoleAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting role with ID {id}.");
                var role= await _authClient.GetFromJsonAsync<Role>($"Role/{id}");
                Console.WriteLine($"RoleID: {role.Id}");
                Console.WriteLine($"Rolenom: {role.RoleNom}");
                //var response = await _authClient.GetStringAsync($"Role/{id}");
                //Console.WriteLine(response);
              //  var role = JsonSerializer.Deserialize<Role>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // var response = await _authClient.GetFromJsonAsync<Subscriber>($"Subscriber/ByUser/{IdUser}");
                // var role = JsonSerializer.Deserialize<Role>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
    }
}
