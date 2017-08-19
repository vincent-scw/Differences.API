db.createUser(
    {
        user: "admin",
        pwd: "abc123!",
        roles: [{ role: "root", db: "admin" }]
    }
);