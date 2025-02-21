export function updateProjectPayload(
  projectData,
  status,
  customer,
  employee,
  service
) {
  // Add debug logging
  console.log("Input projectData:", projectData);

  // Find all required objects with their complete structure
  const statusObj = status.find((s) => s.id === Number(projectData.statusId));
  const customerObj = customer.find(
    (c) => c.id === Number(projectData.customerId)
  );
  const employeeObj = employee.find(
    (e) => e.id === Number(projectData.employeeId)
  );
  const serviceObj = service.find(
    (s) => s.id === Number(projectData.serviceId)
  );

  // Get the service employee with roles
  const serviceEmployee = employee.find((e) => e.id === serviceObj?.employeeId);

  const project = {
    id: projectData.id,
    projectNumber: projectData.projectNumber,
    name: projectData.name,
    description: projectData.description,
    startDate: projectData.startDate,
    endDate: projectData.endDate,
    priority: projectData.priority,
    statusId: Number(projectData.statusId),
    customerId: Number(projectData.customerId),
    employeeId: Number(projectData.employeeId),
    serviceId: Number(projectData.serviceId),
    status: statusObj,
    customers: customerObj,
    employee: employeeObj,
    service: {
      ...serviceObj,
      employee: {
        ...serviceEmployee,
        roles: serviceEmployee?.roles || {
          id: 11,
          roleName: "Product Owner",
        },
      },
    },
  };

  // Add debug logging for the final payload
  console.log("Generated payload:", project);
  return project;
}

export function createProjectPayload(projectData) {
  return {
    projectNumber: crypto.randomUUID(),
    name: projectData.name,
    description: projectData.description,
    startDate: projectData.startDate,
    endDate: projectData.endDate,
    priority: projectData.priority,
    statusId: Number(projectData.statusId),
    customerId: Number(projectData.customerId),
    employeeId: Number(projectData.employeeId),
    serviceId: Number(projectData.serviceId),
  };
}

export function mapProjectData(projects, status, customer, employee, service) {
  return projects.map((proj) => {
    const cust = customer.find((c) => c.id === proj.customerId);
    const emp = employee.find((e) => e.id === proj.employeeId);
    return {
      ...proj,
      status: status.find((s) => s.id === proj.statusId)?.status || "",
      service: service.find((s) => s.id === proj.serviceId)?.serviceName || "",
      serviceCurrency: service.currency || "",
      serviceUnit: service.unit || "",
      customers: cust || {},
      employee: emp || {},
    };
  });
}


export async function fetchProjects(
  searchTerm,
  status,
  customer,
  employee,
  service
) {
  try {
    const response = await fetch(
      `http://192.168.1.6:5000/api/projects?search=${searchTerm || ""}`
    );
    if (!response.ok) throw new Error("Failed to fetch projects");
    const data = await response.json();
    return mapProjectData(data, status, customer, employee, service);
  } catch (error) {
    console.error("Error in fetchProjects:", error);
    throw error;
  }
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
