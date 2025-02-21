const API_URL = "http://192.168.1.6:5000/api/services";

import { formatCurrency } from "@/utils/currencyFormatter";

export function mapServiceData(services, employees, units, currencies) {
  return services.map((service) => {
    const emp = employees.find((e) => e.id === service.employeeId);
    const unitObj = units.find((u) => u.id === service.unitId);
    const currencyObj = currencies.find((c) => c.id === service.currencyId);
    return {
      ...service,
      unit: unitObj ? unitObj.unit : "",
      employees: emp || {},
      currency: currencyObj ? currencyObj.currency : "",
      formattedPrice: formatCurrency(service.price),
      formattedStartupPrice: formatCurrency(service.startupPrice),
    };
  });
}

export function createUpdatePayload(serviceData, units, currencies, employees) {
  const payload = {
    ...serviceData,
    startupPrice: Number(serviceData.startupPrice),
    price: Number(serviceData.price),
    unitId: Number(serviceData.unitId),
    currencyId: Number(serviceData.currencyId),
    employeeId: Number(serviceData.employeeId),
  };

  // Look up nested objects
  const unitObj = units.find((u) => u.id === payload.unitId);
  const currencyObj = currencies.find((c) => c.id === payload.currencyId);
  const employeeObj = employees.find((e) => e.id === payload.employeeId);

  payload.units = unitObj ? { ...unitObj } : null;
  payload.currencies = currencyObj ? { ...currencyObj } : null;
  payload.employee = employeeObj ? { ...employeeObj } : null;

  return payload;
}

export async function checkServiceUsage(serviceId) {
  const response = await fetch(
    `http://192.168.1.6:5000/api/projects?serviceId=${serviceId}`
  );
  if (!response.ok) {
    throw new Error("Failed to check service usage");
  }
  const projects = await response.json();
  return projects.filter((p) => p.serviceId === serviceId);
}

export async function fetchServices(employees, units, currencies) {
  try {
    const response = await fetch(`${API_URL}?`);
    if (!response.ok) throw new Error("Failed to fetch services");
    const rawServices = await response.json();
    return mapServiceData(rawServices, employees, units, currencies);
  } catch (error) {
    console.error("Error in fetchServices:", error);
    throw error;
  }
}

async function createService(payload) {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (response.status === 409) {
    throw new Error("Service already exists");
  }
  if (!response.ok) throw new Error("Create failed");

  return await response.json();
}

async function updateService(payload) {
  const response = await fetch(API_URL, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (response.status === 409) {
    throw new Error("Service already exists");
  }
  if (!response.ok) throw new Error("Update failed");

  return await response.json();
}

export async function createNewService(
  editedItem,
  units,
  currencies,
  employees
) {
  try {
    const payload = createUpdatePayload(
      editedItem,
      units,
      currencies,
      employees
    );
    return await createService(payload);
  } catch (error) {
    console.error("Create service error:", error);
    throw error;
  }
}

export async function updateExistingService(
  editedItem,
  units,
  currencies,
  employees
) {
  try {
    const payload = createUpdatePayload(
      editedItem,
      units,
      currencies,
      employees
    );
    return await updateService(payload);
  } catch (error) {
    console.error("Update service error:", error);
    throw error;
  }
}

export async function deleteService(id) {
  try {
    console.log(`Attempting to delete service with ID: ${id}`);
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    });

    const responseText = await response.text();
    console.log("Delete response:", {
      status: response.status,
      text: responseText,
    });

    if (!response.ok) {
      const errorMessage = responseText
        ? JSON.parse(responseText)
        : "Delete failed";
      throw new Error(errorMessage.message || errorMessage);
    }

    return true;
  } catch (error) {
    console.error("Delete service error:", error);
    throw error;
  }
}
