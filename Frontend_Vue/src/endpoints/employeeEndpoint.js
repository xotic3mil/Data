import { format, parseISO } from "date-fns";

const API_URL = "http://192.168.1.6:5000/api";

export async function fetchEmployees(searchTerm = "") {
  try {
    const url = `${API_URL}/employees/search?search=${encodeURIComponent(
      searchTerm
    )}`;
    const response = await fetch(url);
    if (!response.ok) throw new Error("Failed to fetch employees");

    const data = await response.json();
    return data.map((emp) => ({
      ...emp,
      roleName: emp.roles ? emp.roles.roleName : "Unknown",
    }));
  } catch (error) {
    console.error("Error fetching employees:", error);
    throw error;
  }
}

export async function createEmployee(employeeData) {
  if (employeeData.contractStartDate) {
    employeeData.contractStartDate = format(
      parseISO(employeeData.contractStartDate),
      "yyyy-MM-dd"
    );
  }

  const response = await fetch(`${API_URL}/employees`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(employeeData),
  });

  if (response.status === 409) {
    throw new Error("Email already exists");
  }
  if (!response.ok) throw new Error("Create failed");

  return await response.json();
}

export async function updateEmployee(employeeData) {
  if (employeeData.contractStartDate) {
    employeeData.contractStartDate = format(
      parseISO(employeeData.contractStartDate),
      "yyyy-MM-dd"
    );
  }

  const response = await fetch(`${API_URL}/employees`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(employeeData),
  });

  if (response.status === 409) {
    throw new Error("Email already exists");
  }
  if (!response.ok) throw new Error("Update failed");

  return await response.json();
}

export async function deleteEmployee(id) {
  const response = await fetch(`${API_URL}/employees/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) throw new Error("Delete failed");
  return true;
}
