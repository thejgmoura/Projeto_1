using MySql.Data.MySqlClient;
namespace CRUD_MySQL
{
	public partial class Form1 : Form
	{
		private string data_source = "datasource = localhost; username = root; password = 1234; database = db_cadastros";
		private int? id_selecionado = null;
		public Form1 ()
		{
			InitializeComponent ();
			configurar_listview ();
			carregar ();
		}
		private void configurar_listview ()
		{
			listView1.View = View.Details;
			listView1.LabelEdit = true;
			listView1.AllowColumnReorder = true;
			listView1.FullRowSelect = true;
			listView1.GridLines = true;
			listView1.Columns.Add ("ID", 30, HorizontalAlignment.Left);
			listView1.Columns.Add ("NOME", 150, HorizontalAlignment.Left);
			listView1.Columns.Add ("TELEFONE", 150, HorizontalAlignment.Left);
			listView1.Columns.Add ("E-MAIL", 150, HorizontalAlignment.Left);
			button3.Visible = false;
			button4.Visible = false;
		}
		private MySqlConnection conectar ()
		{
			return new MySqlConnection (data_source);
		}
		private void exibir_erros (string mensagem)
		{
			MessageBox.Show ($"Erro! {mensagem}", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		private void executarcomandosql (string comandoSQL, Dictionary <string, object> indicador)
		{
			try
			{
				using (MySqlConnection con = conectar ())
				{
					con.Open ();
					using (MySqlCommand comando = new MySqlCommand (comandoSQL, con))
					{
						foreach (var param in indicador)
						{
							comando.Parameters.AddWithValue (param.Key, param.Value);
						}
						comando.ExecuteNonQuery ();
					}
				}
			}
			catch (MySqlException ex)
			{
				exibir_erros (ex.Message);
			}
			catch (Exception ex)
			{
				exibir_erros (ex.Message);
			}
		}
		private void carregarnolistview (string query, Dictionary <string, object> indicador = null)
		{
			try
			{
				using (MySqlConnection con = conectar ())
				{
					con.Open ();
					using (MySqlCommand comando = new MySqlCommand (query, con))
					{
						if (indicador != null)
						{
							foreach (var param in indicador)
							{
								comando.Parameters.AddWithValue (param.Key, param.Value);
							}
						}
						using (MySqlDataReader reader = comando.ExecuteReader ())
						{
							listView1.Items.Clear ();
							while (reader.Read ())
							{
								var row = new string []
								{
									reader.GetInt32 (0).ToString (),
									reader.GetString (1),
									reader.GetString (2),
									reader.GetString (3),
								};
								listView1.Items.Add (new ListViewItem (row));
							}
						}
					}
				}
			}
			catch (MySqlException ex)
			{
				exibir_erros (ex.Message);
			}
			catch (Exception ex)
			{
				exibir_erros (ex.Message);
			}
		}
		private void carregar ()
		{
			string query = "SELECT * FROM new_table";
			carregarnolistview (query);
		}
		private void limpar_campos ()
		{
			id_selecionado = null;
			textBox1.Clear ();
			textBox2.Clear ();
			textBox3.Clear ();
			textBox4.Clear ();
			textBox1.Focus ();
			button3.Visible = false;
			button4.Visible = false;
		}
		private void excluir ()
		{
			try
			{
				var conf = MessageBox.Show ("Certeza disso?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (conf == DialogResult.Yes)
				{
					var indicador = new Dictionary <string, object> { { "@id", id_selecionado } };
					executarcomandosql ("DELETE FROM new_table WHERE id = @id", indicador);
					MessageBox.Show ("Apagado!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
					carregar ();
					limpar_campos ();
				}
			}
			catch (Exception ex)
			{
				exibir_erros (ex.Message);
			}
		}
		private void button1_Click (object sender, EventArgs e)
		{
			var indicador = new Dictionary <string, object>
			{
				{"@nome", textBox1.Text},
				{"@telefone", textBox2.Text},
				{"@email", textBox3.Text}
			};
			executarcomandosql ("INSERT INTO new_table (nome, telefone, email) VALUES (@nome, @telefone, @email)", indicador);
			MessageBox.Show ("Cadastro feito!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			carregar ();
			limpar_campos ();
		}
		private void button2_Click (object sender, EventArgs e)
		{
			string query = "SELECT * FROM new_table WHERE nome LIKE @q";
			var indicador = new Dictionary <string, object> { { "@q", "%" + textBox4.Text + "%" } };
			carregarnolistview (query, indicador);
		}
		private void listView1_ItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				id_selecionado = Convert.ToInt32 (e.Item.SubItems [0].Text);
				textBox1.Text = e.Item.SubItems [1].Text;
				textBox2.Text = e.Item.SubItems [2].Text;
				textBox3.Text = e.Item.SubItems [3].Text;
				button3.Visible = true;
				button4.Visible = true;
			}
		}
		private void button3_Click (object sender, EventArgs e)
		{
			excluir ();
		}
		private void button4_Click (object sender, EventArgs e)
		{
			var indicador = new Dictionary <string, object>
			{
				{"@nome", textBox1.Text},
				{"@telefone", textBox2.Text},
				{"@email", textBox3.Text},
				{"@id", id_selecionado}
			};
			executarcomandosql ("UPDATE new_table SET nome = @nome, telefone = @telefone, email = @email WHERE id = @id", indicador);
			MessageBox.Show ("Atualizado!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			carregar ();
			limpar_campos ();
		}
	}
 }
