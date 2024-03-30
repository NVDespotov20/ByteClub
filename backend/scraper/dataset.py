import psycopg2

conn: psycopg2.extensions.connection = psycopg2.connect("host=byteclubdb.postgres.database.azure.com dbname=postgres user=qp password=QnakaPalqka123 sslmode=require")
curr: psycopg2.extensions.cursor = conn.cursor()

with open("dataset.csv", "r", encoding='utf-8') as file:
    content = file.read()
    #delete first line

    for line in content.split("\n")[1:]:
        if line:
            split = line.split(",")
            name = split[1]
            description = split[10]
            category = split[4]
            imate = split[15]

            curr.execute("""INSERT INTO "Products" ("Name", "Description", "Category", "Image")
                        VALUES 
                            (%s, %s, %s, %s);""", (name, description, category, imate))
            
    conn.commit()