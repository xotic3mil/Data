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

export function projectPayload(projectData) {
  // Add debug logging
  console.log("Input projectData:", projectData);

  const project = {
    Id: projectData.id,
    ProjectNumber: projectData.projectNumber,
    Name: projectData.name,
    Description: projectData.description,
    StartDate: projectData.startDate,
    EndDate: projectData.endDate,
    Priority: projectData.priority,
    StatusId: Number(projectData.statusId),
    CustomerId: Number(projectData.customerId),
    EmployeeId: Number(projectData.employeeId),
    ServiceId: Number(projectData.serviceId),
    Status: projectData.status
      ? {
          Id: projectData.status.id ? parseInt(projectData.status.id) : null,
          Status: projectData.status.status,
        }
      : null,
    Customers: projectData.customers
      ? {
          Id: Number(projectData.customers.id),
          CompanyName: projectData.customers.companyName,
          CustomerContactPersonId: Number(
            projectData.customers.customerContactPersonId
          ),
          CustomerContactPerson: projectData.customers.customerContactPerson
            ? {
                Id: Number(projectData.customers.customerContactPerson.id),
                FirstName:
                  projectData.customers.customerContactPerson.firstName,
                LastName: projectData.customers.customerContactPerson.lastName,
                Email: projectData.customers.customerContactPerson.email,
                Phone: projectData.customers.customerContactPerson.phone,
              }
            : null,
        }
      : null,
    Employee: projectData.employee
      ? {
          Id: Number(projectData.employee.id),
          FirstName: projectData.employee.firstName,
          LastName: projectData.employee.lastName,
          Phone: projectData.employee.phone,
          Email: projectData.employee.email,
          ContractStartDate: projectData.employee.contractStartDate,
          RoleId: Number(projectData.employee.roleId),
          Roles: projectData.employee.roles
            ? {
                Id: Number(projectData.employee.roles.id),
                RoleName: projectData.employee.roles.roleName,
              }
            : null,
        }
      : null,
    Service: projectData.service
      ? {
          Id: Number(projectData.service.id),
          ServiceName: projectData.service.serviceName,
          ServiceDescription: projectData.service.serviceDescription,
          StartupPrice: Number(projectData.service.startupPrice),
          Price: Number(projectData.service.price),
          UnitId: Number(projectData.service.unitId),
          CurrencyId: Number(projectData.service.currencyId),
          EmployeeId: Number(projectData.service.employeeId),
          Units: projectData.service.units,
          Currencies: projectData.service.currencies,
          Employee: projectData.service.employee,
        }
      : null,
  };

  // Add debug logging for the final payload
  console.log("Generated payload:", project);
  return {
    projects: project,
  };
}

export async function updateProject(payload) {
  try {
    console.log("Update payload:", JSON.stringify(payload, null, 2));
    const response = await fetch("http://192.168.1.6:5000/api/projects", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      const errorText = await response.text();
      console.error("Server response:", errorText);
      throw new Error(errorText || "Project update failed");
    }

    return response.json();
  } catch (error) {
    console.error("Error in updateProject:", error);
    throw error;
  }
}

export async function createProject(payload) {
  try {
    const response = await fetch("http://192.168.1.6:5000/api/projects", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });
    if (response.status === 409) {
      throw new Error("Project already exists");
    }
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Project creation failed");
    }
    return response.json();
  } catch (error) {
    console.error("Error in createProject:", error);
    throw error;
  }
}

export async function deleteProject(id) {
  try {
    const response = await fetch(`http://192.168.1.6:5000/api/projects/${id}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      throw new Error("Delete failed");
    }

    // If the server returns 204 No Content, simply return null.
    if (response.status === 204) {
      return null;
    }

    // Alternatively, check the content-length header or response text.
    const text = await response.text();
    return text ? JSON.parse(text) : null;
  } catch (error) {
    console.error("Error in deleteProject:", error);
    throw error;
  }
}
