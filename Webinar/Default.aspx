<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Webinar.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style>
    .botao {
        background: #ef4136;
        border: 0;
        padding: 10px 40px;
        color: #fff;
        transition: 0.4s;
        border-radius: 50px;
        cursor: pointer;
    }
    .botaosub {
          color: #fff;
          background: transparent;
          padding: 7px 22px;
          border-radius: 50px;
          border: 2px solid #ef4136;
          transition: all ease-in-out 0.3s;
          font-weight: 500;
          margin-left: 8px;
          margin-top: 2px;
          line-height: 1;
          font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==========================
    Intro Section
  ============================-->
    <section id="intro">
    <div class="intro-container wow fadeIn">
      <h1 runat="server" id="lblTitulo"></h1>
      <p runat="server" class="mb-4 pb-0" id="lblSubTitulo"></p>
      <a runat="server" id="lblLink" href="#" class="venobox play-btn mb-4" data-vbtype="video"
        data-autoplay="true"></a>
      <a href="#about" class="about-btn scrollto">Mais sobre a Aggregate</a>
    </div>
  </section>

    <!--==========================
        Sessão Entrar
    ============================-->
    <section id="login" class="modal fade">
            <div class="modal-dialog modal-login">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="avatar">
                            <img src="img/Users.png" alt="Avatar" />
                        </div>
                        <h4 class="modal-title"><span class="fa fa-unlock-alt"></span> Login</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>                        
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:Login ID="Login1" runat="server" Width="100%"><LayoutTemplate>
                            <label for="usrname"><span class="fa fa-envelope"></span> E-mail</label>
                            <asp:TextBox runat="server" class="form-control" ID="UserName" placeholder="Informe o E-mail"/><br />                               
                            <label for="psw"><span class="fa fa-key"></span> Senha</label>
                            <asp:TextBox runat="server" class="form-control" ID="Password" TextMode="Password" placeholder="Informe a senha"/><br />
                            <asp:LinkButton ID="btnEntrarLogin" OnClick="btnEntrarLogin_Click" runat="server" BackColor="Green" CssClass="btn btn-primary btn-lg btn-block login-btn"><i class="fa fa-power-off"></i>&nbsp;Entrar</asp:LinkButton>                            
                            </LayoutTemplate></asp:Login>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <ul style="list-style:none; margin-left:-40px">
                            <li><a class="pull-left" data-dismiss="modal" data-toggle="modal" href="#cadastrar">Não possuí cadastro?</a></li>
                            <li><a>▬</a><br /></li>
                            <li><a class="pull-left" data-dismiss="modal" data-toggle="modal" href="#recuperar">Esqueceu a Senha?</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </section>

    <!--==========================
        Sessão Cadastrar
    ============================-->
    <section id="cadastrar" class="modal fade">
            <div class="modal-dialog modal-login">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="avatar">
                            <img src="img/Users.png" alt="Avatar" />
                        </div>
                        <h4 class="modal-title"><span class="fa fa-user-plus"></span> Cadastrar</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>                        
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="usrname"><span class="fa fa-user"></span> Nome</label><br />
                            <asp:TextBox runat="server" TextMode="SingleLine" class="form-control" id="txtCadastrarNome" placeholder="Digte o Nome"/>
                            <br />
                            <label for="usrname"><span class="fa fa-envelope"></span> E-mail</label><br />
                            <asp:TextBox runat="server" TextMode="Email" class="form-control" ID="txtCadastrarEmail" placeholder="Digite o E-mail"/>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCadastrarEmail" ErrorMessage="E-mail inválido." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <br />                                
                            <label for="psw"><span class="fa fa-key"></span> Senha</label><br />
                            <asp:TextBox runat="server" TextMode="Password" class="form-control" id="txtCadastrarSenha" placeholder="Digite a senha"/>
                        </div>                            
                        <asp:LinkButton ID="btnCadastrar" OnClick="btnCadastrarUsuario_Click" runat="server" CssClass="btn btn-primary btn-lg btn-block login-btn"><i class="fa fa-power-off"></i>&nbsp;Cadastrar</asp:LinkButton>                        
                    </div>
                    <div class="modal-footer">
                        <a class="pull-left" data-dismiss="modal" data-toggle="modal" href="#login">Já possuí cadastro?</a>                        
                    </div>
                </div>
            </div>
        </section>

    <!--==========================
      Sessão Recuperar Senha
    ============================-->
    <section id="recuperar" class="modal fade">
            <div class="modal-dialog modal-login">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="avatar">
                            <img src="img/Users.png" alt="Avatar" />
                        </div>
                        <h4 class="modal-title"><span class="fa fa-recycle"></span> Recuperar Senha</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">          
                            <label for="usrname"><span class="fa fa-envelope"></span> E-mail</label><br />
                            <asp:TextBox runat="server" TextMode="Email" class="form-control" ID="txtEmailRecuperar" placeholder="Digite o E-mail"/>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailRecuperar" ErrorMessage="E-mail inválido." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <br />
                        </div>                            
                        <asp:LinkButton ID="btnRecuperarSenha" OnClick="btnRecuperarSenha_Click" runat="server" CssClass="btn btn-primary btn-lg btn-block login-btn"><i class="fa fa-power-off"></i>&nbsp;Enviar</asp:LinkButton>                        
                    </div>                    
                </div>
            </div>
        </section>

    <!--==========================
      Sessão Sobre
    ============================-->
    <section id="about">
      <div class="container">
        <div class="row">
          <div class="col-lg-6">
            <h2>Quem somos</h2>
            <p runat="server" id="lblQuemSomos"></p>
          </div>
          <div class="col-lg-3">
            <h3>Quando</h3>
            <p runat="server" id="lblQuando"></p>
          </div>
          <div class="col-lg-3">
            <h3>Onde</h3>
            <p runat="server" id="lblOnde"></p>
          </div>
        </div>
      </div>
    </section>
    
    <!--==========================
      Sessão Eventos
    ============================-->
    <section id="venue" class="wow fadeInUp">

      <div class="container-fluid">

        <div class="section-header">
          <h2>Eventos</h2>
          <p>Galeria</p>
        </div>        

      </div>

      <div class="container-fluid venue-gallery-container">
        <div class="row no-gutters">

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/1.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/1.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/2.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/2.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/3.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/3.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/4.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/4.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/5.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/5.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/6.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/6.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/7.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/7.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

          <div class="col-lg-3 col-md-4">
            <div class="venue-gallery">
              <a href="img/venue-gallery/8.jpg" class="venobox" data-gall="venue-gallery">
                <img src="img/venue-gallery/8.jpg" alt="" class="img-fluid">
              </a>
            </div>
          </div>

        </div>
      </div>

    </section>

    <!--==========================
      Sessão Palestrantes
    ============================-->
    <section id="speakers">
      <div class="container">
        <div class="section-header">
          <h2>Palestrantes</h2>
          <p>Aqui estão alguns de nossos palestrantes</p>
        </div>

        <div class="row">
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante1"  src="#" alt="Speaker 1" class="img-fluid" >
              <div class="details">
                <h3><a runat="server" id="aPalestrante1" href="#"></a></h3>
                <p runat="server" id="pPalestrante1">O outro incidente</p>
                <div class="social">
                  <a runat="server" id="twtPalestrante1" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante1" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante1" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante1" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante2" src="#" alt="Speaker 2" class="img-fluid">
              <div class="details">
                <h3><a runat="server" id="aPalestrante2" href="#"></a></h3>
                <p runat="server" id="pPalestrante2"></p>
                <div class="social">
                  <a runat="server" id="twtPalestrante2" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante2" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante2" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante2" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante3" src="#" alt="Speaker 3" class="img-fluid">
              <div class="details">
                <h3><a runat="server" id="aPalestrante3" href="#"></a></h3>
                <p runat="server" id="pPalestrante3"></p>
                <div class="social">
                  <a runat="server" id="twtPalestrante3" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante3" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante3" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante3" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante4" src="#" alt="Speaker 4" class="img-fluid">
              <div class="details">
                <h3><a runat="server" id="aPalestrante4" href="#"></a></h3>
                <p runat="server" id="pPalestrante4"></p>
                <div class="social">
                  <a runat="server" id="twtPalestrante4" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante4" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante4" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante4" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante5" src="#" alt="Speaker 5" class="img-fluid">
              <div class="details">
                <h3><a runat="server" id="aPalestrante5" href="#"></a></h3>
                <p runat="server" id="pPalestrante5"></p>
                <div class="social">
                  <a runat="server" id="twtPalestrante5" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante5" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante5" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante5" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-4 col-md-6">
            <div class="speaker" style="width: 350px; height: 350px;">
              <img runat="server" id="imgPalestrante6" src="#" alt="Speaker 6" class="img-fluid">
              <div class="details">
                <h3><a runat="server" id="aPalestrante6" href="#"></a></h3>
                <p runat="server" id="pPalestrante6"></p>
                <div class="social">
                  <a runat="server" id="twtPalestrante6" href="#"><i class="fa fa-twitter"></i></a>
                  <a runat="server" id="facePalestrante6" href="#"><i class="fa fa-facebook"></i></a>
                  <a runat="server" id="ggPalestrante6" href="#"><i class="fa fa-google-plus"></i></a>
                  <a runat="server" id="inPalestrante6" href="#"><i class="fa fa-linkedin"></i></a>
                </div>
              </div>
            </div>
          </div>
    <br /><br />
        </div>
      </div>
    </section>

    <!--==========================
      Sessão Calendário
    ============================-->
    <%--<section id="schedule" class="section-with-bg">
      <div class="container wow fadeInUp">
        <div class="section-header">
          <h2>Calendário de Eventos</h2>
          <p>Aqui está nossa programação de eventos</p>
        </div>

        <ul class="nav nav-tabs" role="tablist">
          <li class="nav-item">
            <a class="nav-link active" href="#day-1" role="tab" data-toggle="tab">Dia 1</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#day-2" role="tab" data-toggle="tab">Dia 2</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#day-3" role="tab" data-toggle="tab">Dia 3</a>
          </li>
        </ul>

        <h3 class="sub-heading">Não, nem vai ocorrer, virei para cumprir as funções dos fracos e corruptos, prazer.
          Quem precisa de um prazer, trabalhar você o encontra.</h3>

        <div class="tab-content row justify-content-center">

          <!-- Schdule Day 1 -->
          <div role="tabpanel" class="col-lg-9 tab-pane fade show active" id="day-1">

            <div class="row schedule-item">
              <div class="col-md-2"><time>09:30 AM</time></div>
              <div class="col-md-10">
                <h4>Registration</h4>
                <p>Fugit voluptas iusto maiores temporibus autem numquam magnam.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>10:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/1.jpg" alt="Brenden Legros">
                </div>
                <h4>Keynote <span>Brenden Legros</span></h4>
                <p>Facere provident incidunt quos voluptas.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>11:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/2.jpg" alt="Hubert Hirthe">
                </div>
                <h4>Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span></h4>
                <p>Maiores dignissimos neque qui cum accusantium ut sit sint inventore.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>12:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/3.jpg" alt="Cole Emmerich">
                </div>
                <h4>Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span></h4>
                <p>Veniam accusantium laborum nihil eos eaque accusantium aspernatur.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>02:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/4.jpg" alt="Jack Christiansen">
                </div>
                <h4>Qui non qui vel amet culpa sequi. <span>Jack Christiansen</span></h4>
                <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>03:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/5.jpg" alt="Alejandrin Littel">
                </div>
                <h4>Quos ratione neque expedita asperiores. <span>Alejandrin Littel</span></h4>
                <p>Eligendi quo eveniet est nobis et ad temporibus odio quo.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>04:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/6.jpg" alt="Willow Trantow">
                </div>
                <h4>Quo qui praesentium nesciunt <span>Willow Trantow</span></h4>
                <p>Voluptatem et alias dolorum est aut sit enim neque veritatis.</p>
              </div>
            </div>

          </div>
          <!-- End Schdule Day 1 -->

          <!-- Schdule Day 2 -->
          <div role="tabpanel" class="col-lg-9  tab-pane fade" id="day-2">

            <div class="row schedule-item">
              <div class="col-md-2"><time>10:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/1.jpg" alt="Brenden Legros">
                </div>
                <h4>Libero corrupti explicabo itaque. <span>Brenden Legros</span></h4>
                <p>Facere provident incidunt quos voluptas.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>11:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/2.jpg" alt="Hubert Hirthe">
                </div>
                <h4>Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span></h4>
                <p>Maiores dignissimos neque qui cum accusantium ut sit sint inventore.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>12:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/3.jpg" alt="Cole Emmerich">
                </div>
                <h4>Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span></h4>
                <p>Veniam accusantium laborum nihil eos eaque accusantium aspernatur.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>02:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/4.jpg" alt="Jack Christiansen">
                </div>
                <h4>Qui non qui vel amet culpa sequi. <span>Jack Christiansen</span></h4>
                <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>03:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/5.jpg" alt="Alejandrin Littel">
                </div>
                <h4>Quos ratione neque expedita asperiores. <span>Alejandrin Littel</span></h4>
                <p>Eligendi quo eveniet est nobis et ad temporibus odio quo.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>04:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/6.jpg" alt="Willow Trantow">
                </div>
                <h4>Quo qui praesentium nesciunt <span>Willow Trantow</span></h4>
                <p>Voluptatem et alias dolorum est aut sit enim neque veritatis.</p>
              </div>
            </div>

          </div>
          <!-- End Schdule Day 2 -->

          <!-- Schdule Day 3 -->
          <div role="tabpanel" class="col-lg-9  tab-pane fade" id="day-3">

            <div class="row schedule-item">
              <div class="col-md-2"><time>10:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/2.jpg" alt="Hubert Hirthe">
                </div>
                <h4>Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span></h4>
                <p>Maiores dignissimos neque qui cum accusantium ut sit sint inventore.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>11:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/3.jpg" alt="Cole Emmerich">
                </div>
                <h4>Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span></h4>
                <p>Veniam accusantium laborum nihil eos eaque accusantium aspernatur.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>12:00 AM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/1.jpg" alt="Brenden Legros">
                </div>
                <h4>Libero corrupti explicabo itaque. <span>Brenden Legros</span></h4>
                <p>Facere provident incidunt quos voluptas.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>02:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/4.jpg" alt="Jack Christiansen">
                </div>
                <h4>Qui non qui vel amet culpa sequi. <span>Jack Christiansen</span></h4>
                <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>03:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/5.jpg" alt="Alejandrin Littel">
                </div>
                <h4>Quos ratione neque expedita asperiores. <span>Alejandrin Littel</span></h4>
                <p>Eligendi quo eveniet est nobis et ad temporibus odio quo.</p>
              </div>
            </div>

            <div class="row schedule-item">
              <div class="col-md-2"><time>04:00 PM</time></div>
              <div class="col-md-10">
                <div class="speaker">
                  <img src="img/speakers/6.jpg" alt="Willow Trantow">
                </div>
                <h4>Quo qui praesentium nesciunt <span>Willow Trantow</span></h4>
                <p>Voluptatem et alias dolorum est aut sit enim neque veritatis.</p>
              </div>
            </div>

          </div>
          <!-- End Schdule Day 2 -->

        </div>

      </div>

    </section>--%>

    <!--==========================
      Sessão Acervo
    ============================-->
    <section id="gallery" class="wow fadeInUp">
      <div class="container">
        <div class="section-header">
          <h2>Acervo</h2>
          <p>Aqui está nosso acervo com palestras gravadas e eventos já ocorridos</p>
        </div>
      </div>

      <div class="owl-carousel gallery-carousel">
        <a href="img/gallery/1.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/1.jpg" alt=""></a>
        <a href="img/gallery/2.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/2.jpg" alt=""></a>
        <a href="img/gallery/3.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/3.jpg" alt=""></a>
        <a href="img/gallery/4.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/4.jpg" alt=""></a>
        <a href="img/gallery/5.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/5.jpg" alt=""></a>
        <a href="img/gallery/6.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/6.jpg" alt=""></a>
        <a href="img/gallery/7.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/7.jpg" alt=""></a>
        <a href="img/gallery/8.jpg" class="venobox" data-gall="gallery-carousel"><img src="img/gallery/8.jpg" alt=""></a>
      </div>

    </section>
    
    <!--==========================
      Sessão Conexão Empresarial
    ============================-->
    <section id="sponsors" class="section-with-bg wow fadeInUp">

      <div class="container">
        <div class="section-header">
          <h2>Conexão empresarial</h2>
        </div>

        <div class="row no-gutters sponsors-wrap clearfix">

          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/1.png" class="img-fluid" alt="">
            </div>
          </div>
          
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/2.png" class="img-fluid" alt="">
            </div>
          </div>
        
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/3.png" class="img-fluid" alt="">
            </div>
          </div>
          
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/4.png" class="img-fluid" alt="">
            </div>
          </div>
          
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/5.png" class="img-fluid" alt="">
            </div>
          </div>
        
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/6.png" class="img-fluid" alt="">
            </div>
          </div>
          
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/7.png" class="img-fluid" alt="">
            </div>
          </div>
          
          <div class="col-lg-3 col-md-4 col-xs-6">
            <div class="sponsor-logo">
              <img src="img/sponsors/8.png" class="img-fluid" alt="">
            </div>
          </div>

        </div>

      </div>

    </section>

    <!--==========================
      Sessão Perguntas Frequentes
    ============================-->
    <section id="faq" class="wow fadeInUp">
      <div class="container">
        <div class="section-header">
          <h2>Perguntas Frequentes </h2>
        </div>
        <div class="row justify-content-center">
          <div class="col-lg-9">
              <ul id="faq-list">

                <li>
                  <a data-toggle="collapse" class="collapsed" href="#faq1" runat="server" id="lblPergunta1"></a>
                  <div id="faq1" class="collapse" data-parent="#faq-list">
                    <p runat="server" id="lblResposta1"></p>
                  </div>
                </li>
                  
                <li>
                  <a runat="server" data-toggle="collapse" href="#faq2" class="collapsed" id="lblPergunta2"></a>
                  <div id="faq2" class="collapse" data-parent="#faq-list">
                    <p runat="server" id="lblResposta2"></p>
                  </div>
                </li>
      
                <li>
                  <a runat="server" data-toggle="collapse" href="#faq3" class="collapsed" id="lblPergunta3"></a>
                  <div id="faq3" class="collapse" data-parent="#faq-list">
                    <p runat="server" id="lblResposta3"></p>
                  </div>
                </li>
      
                <li>
                  <a runat="server" data-toggle="collapse" href="#faq4" class="collapsed" id="lblPergunta4"></a>
                  <div id="faq4" class="collapse" data-parent="#faq-list">
                    <p runat="server" id="lblResposta4"></p>
                  </div>
                </li>               
      
                <li>
                  <a runat="server" data-toggle="collapse" href="#faq6" class="collapsed" id="lblPergunta5"></a>
                  <div id="faq6" class="collapse" data-parent="#faq-list">
                    <p runat="server" id="lblResposta5"></p>
                  </div>
                </li>

                  <li>
                  <a data-toggle="collapse" href="#faq5" class="collapsed">Quem devo contatar se ainda tiver dúvidas?<i class="fa fa-minus-circle"></i></a>
                  <div id="faq5" class="collapse" data-parent="#faq-list">
                    <p>Para perguntas gerais, entre em contato através de alguns de nossos meios de contato. <a href="#contact">Clique aqui</a> Para ser direcionado à área de contato.
                  </div>
                </li>
      
              </ul>
          </div>
        </div>

      </div>

    </section>

    <!--==========================
      Sessão Subscribe
    ============================-->
    <section id="subscribe">
      <div class="container">
        <div class="section-header">
          <h2>A gente te avisa</h2>
          <p>Nos informe seu e-mail para receber informações com novos seminários e eventos.</p>
        </div>
        <div class="form-row justify-content-center">
            <div class="col-auto">
              <asp:TextBox ID="txtEnviarEmail" runat="server" TextMode="Email" class="form-control" placeholder="Digite o E-mail" />
            </div>
        </div><br />
        <div class="form-row justify-content-center">
            <div class="col-auto">
                <%--<asp:Button runat="server" ID="btnEnviarNotificacao" OnClick="btnEnviarNotificacao_Click" CssClass="botao" LogoutAction="Refresh" PostBackUrl="#subscribe" Text="Inscrever-se" />--%>
              <asp:LinkButton ID="vbtnEnviarNotificacao" OnClick="btnEnviarNotificacao_Click" runat="server" CssClass="botaosub" LogoutAction="Refresh" PostBackUrl="#subscribe">Inscrever-se</asp:LinkButton>                
            </div>
          </div>
      </div>
    </section>

    <!--==========================
      Sessão Contato
    ============================-->
    <section id="contact" class="section-bg wow fadeInUp">

      <div class="container">

        <div class="section-header">
          <h2>Contate-nos</h2>
          <p>Nos contate quando quiser, será um prazer atende-lo.</p>
        </div>

        <div class="row contact-info">

          <div class="col-md-4">
            <div class="contact-address">
              <i class="ion-ios-location-outline"></i>
              <h3>Endereço</h3>
              <address runat="server" id="lblEndereco"></address>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-phone">
              <i class="ion-ios-telephone-outline"></i>
              <h3>Telefone</h3>
              <p runat="server" id="lblTelefone"><a id="lblTelefone1"</a></p>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-email">
              <i class="ion-ios-email-outline"></i>
              <h3>E-mail</h3>
              <p runat="server" id="lblEmail"><a id="lblEmail1"></a></p>
            </div>
          </div>

        </div>
        <div class="text-center"><asp:Label runat="server" ID="lblContato"></asp:Label><br /><br /></div>        
        <div class="contactForm">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <asp:TextBox runat="server" TextMode="SingleLine" CssClass="form-control" ID="txtNomeContato" placeholder="Seu nome" data-rule="minlen:4" data-msg="Por favor insira ao menos 4 caracteres para o nome."></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <asp:TextBox runat="server" TextMode="Email" CssClass="form-control" name="email" ID="txtEmailContato" placeholder="Seu E-mail" data-rule="email" data-msg="por favor insira um e-mail válido." />
                </div>
            </div>
            <div class="form-group">
                <asp:TextBox runat="server" TextMode="SingleLine" CssClass="form-control" name="subject" ID="txtTituloContato" placeholder="Titulo" data-rule="minlen:4" data-msg="Por favor insira ao menos 8 caracteres para o titulo." />
            </div>
            <div class="form-group">
                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" name="message" ID="txtMensagemContato" rows="5" data-rule="required" data-msg="por favor escreva algo para nós." placeholder="Mensagem" />
            </div>
            <div class="text-center">
                <asp:Button runat="server" CssClass="botao" OnClick="btnEnviarMensagemContato_Click" ID="btnEnviarMensagemContato" PostBackUrl="#contact" Text="Enviar Mensagem" />              
            </div>
          </div>
        </div>
        <br />
    </section>
</asp:Content>
