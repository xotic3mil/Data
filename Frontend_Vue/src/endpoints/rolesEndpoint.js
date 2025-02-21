const API_URL = "http://192.168.1.6:5000/api/roles/";

export async function fetchRoles() {
  try {
    const response = await fetch(`${API_URL}`);
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Failed to fetch roles");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching roles:", error);
    throw error;
  }
}

export async function createRole(roleData) {
  try {
    if (!roleData?.roleName) {
      throw new Error("Role name is required");
    }
    const response = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(roleData),
    });

    if (response.status === 409) {
      throw new Error("Role already exists");
    }
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Create failed");
    }

    return await response.json();
  } catch (error) {
    console.error("Error creating role:", error);
    throw error;
  }
}

export async function updateRole(roleData) {
  try {
    if (!roleData?.id) {
      throw new Error("Role ID is required");
    }
    if (!roleData?.roleName) {
      throw new Error("Role name is required");
    }

    const response = await fetch(API_URL, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(roleData),
    });

    if (response.status === 409) {
      throw new Error("Role with this name already exists");
    }
    if (response.status === 404) {
      throw new Error("Role not found");
    }
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Update failed");
    }
    return await response.json();
  } catch (error) {
    console.error("Error updating role:", error);
    throw error;
  }
}

export async function deleteRole(id) {
  try {
    if (!id) {
      throw new Error("Role ID is required");
    }
    const response = await fetch(`${API_URL}${id}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });
    if (response.status === 404) {
      throw new Error("Role not found");
    }
    if (response.status === 409) {
      throw new Error("Cannot delete role: It is being used");
    }
    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Delete failed");
    }
    return true;
  } catch (error) {
    console.error("Error deleting role:", error);
    throw error;
  }
}
