export async function fetchStatuses() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/status");
    if (!response.ok) throw new Error("Failed to fetch status");
    return response.json();
  } catch (error) {
    console.error("Error in fetchStatuses:", error);
    throw error;
  }
}

export async function fetchCustomerContact() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/contactperson");
    if (!response.ok)
      throw new Error("Failed to fetch customer contact persons");
    const data = await response.json();
    return data.map((cp) => ({
      ...cp,
      fullName: cp.firstName + " " + cp.lastName,
    }));
  } catch (error) {
    console.error("Error in fetchCustomerContact:", error);
    throw error;
  }
}

export async function fetchEmployees() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/employees");
    if (!response.ok) throw new Error("Failed to fetch employees");
    const data = await response.json();
    return data.map((emp) => ({
      ...emp,
      fullName: emp.firstName + " " + emp.lastName,
      email: emp.email,
    }));
  } catch (error) {
    console.error("Error in fetchEmployees:", error);
    throw error;
  }
}

export async function fetchServices() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/services");
    if (!response.ok) throw new Error("Failed to fetch services");
    return response.json();
  } catch (error) {
    console.error("Error in fetchServices:", error);
    throw error;
  }
}

export async function fetchCurrencies() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/currencies");
    if (!response.ok) throw new Error("Failed to fetch currencies");
    return response.json();
  } catch (error) {
    console.error("Error in fetchCurrencies:", error);
    throw error;
  }
}

export async function fetchUnits() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/units");
    if (!response.ok) throw new Error("Failed to fetch units");
    return response.json();
  } catch (error) {
    console.error("Error in fetchUnits:", error);
    throw error;
  }
}

export async function fetchCustomers() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/customers");
    if (!response.ok) throw new Error("Failed to fetch customers");
    return response.json();
  } catch (error) {
    console.error("Error in fetchCustomers:", error);
    throw error;
  }
}

export async function fetchProjects() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/projects");
    if (!response.ok) throw new Error("Failed to fetch projects");
    return response.json();
  } catch (error) {
    console.error("Error in fetchProjects:", error);
    throw error;
  }
}

export async function fetchRevenue() {
  try {
    const projectsData = await fetchProjects();
    const servicesData = await fetchServices();
    let revenue = 0;
    projectsData.forEach((project) => {
      const service = servicesData.find((s) => s.id === project.serviceId);
      if (service && service.price) {
        revenue += parseFloat(service.price);
      }
    });
    return revenue;
  } catch (error) {
    console.error("Error in fetchRevenue:", error);
    throw error;
  }
}

/// Roles MANAGEMENT

const rolesUrl = "http://192.168.1.6:5000/api/roles";


export async function fetchRoles() {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/roles");
    if (!response.ok) throw new Error("Failed to fetch roles");
    return response.json();
  } catch (error) {
    console.error("Error in fetchCurrencies:", error);
    throw error;
  }
}

export async function createRole(role) {
  try {
    const response = await fetch(rolesUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(role),
    });
    if (response.status === 409) {
      throw new Error("Role already exists");
    }
    if (!response.ok) throw new Error("Creation failed");
    return response.json();
  } catch (error) {
    console.error("Error in createRole:", error);
    throw error;
  }
}

export async function updateRole(role) {
  try {
    const response = await fetch(rolesUrl, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(role),
    });
    if (response.status === 409) {
      throw new Error("Role already exists");
    }
    if (!response.ok) throw new Error("Update failed");
    return response.json();
  } catch (error) {
    console.error("Error in updateRole:", error);
    throw error;
  }
}

export async function deleteRole(id) {
  try {
    const response = await fetch(`${rolesUrl}/${id}`, {
      method: "DELETE",
    });
    if (!response.ok) throw new Error("Delete failed");
    return response.json();
  } catch (error) {
    console.error("Error in deleteRole:", error);
    throw error;
  }
}

export async function fetchStats() {
  try {
    const [employeesData, servicesData, projectsData, customersData, revenue] =
      await Promise.all([
        fetchEmployees(),
        fetchServices(),
        fetchProjects(),
        fetchCustomers(),
        fetchRevenue(),
      ]);

    return {
      employees: employeesData.count || employeesData.length || 0,
      services: servicesData.count || servicesData.length || 0,
      projects: projectsData.count || projectsData.length || 0,
      customers: customersData.count || customersData.length || 0,
      revenue,
    };
  } catch (error) {
    console.error("Error in fetchStats:", error);
    throw error;
  }
}
